using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Concrete.Repositories;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class LotController : Controller
    {
        #region Repositories

        private readonly ILotRepository _lotRepository;

        private readonly IBidRepository _bidRepository;

        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        #endregion


        #region Constructor and property

        public int PageSize { get; } = 4;

        public LotController(ILotRepository lotRepository, IBidRepository bidRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _lotRepository = lotRepository;

            _bidRepository = bidRepository;

            _userRepository = userRepository;

            _categoryRepository = categoryRepository;
        }

        #endregion


        #region ActionMethods

        public ViewResult List(string category, int page = 1)
        {
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = _lotRepository.Lots
                    .Where(p => p.IsEnded == false)
                    .Where(p => category == null || p.Category.Name == category)
                    .OrderBy(lot => lot.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new LotPageInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        _lotRepository.Lots.Count() :
                        _lotRepository.Lots.Count(lot => lot.Category.Name == category && lot.IsEnded == false)
                },
                CurrentCategory = category
            };

            return View(model);
        }

        #region Create

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            var categoryList = _categoryRepository.Categories;

            var dropDownCategories = _categoryRepository.Categories
                .Select(categ => new SelectListItem { Text = categ.Name, Value = categ.Name }).ToList();

            ViewBag.Category = dropDownCategories;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LotModel model)
        {
            if (ModelState.IsValid)
            {
                Lot lot = new Lot
                {
                    Title = model.Title,
                    Description = model.Description,
                    CategoryId = _categoryRepository.GetCategoryIdByName(model.Category),
                    CurrentPrice = model.CurrentPrice,
                    GoldPrice = model.GoldPrice,
                    UploadDate = DateTime.Now,
                    EndOfTranding = DateTime.Now.AddDays(30),
                    UserId = _userRepository.GetUserIdByEmail(User.Identity.Name),
                    Photo = model.Image,
                    IsEnded = false,
                    ViewCount = 0
                };

                _lotRepository.SaveLot(lot);
            }

            return RedirectToAction("List", "Lot"); 
        }

        #endregion


        #region Update

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult Update(long id)
        {
            var lot = _lotRepository.GetLotById(id);

            TempData["id"] = id;

            var model = new LotModel
            {
                Title = lot.Title,
                Description = lot.Description
            };


            return View(model);
        }

        [HttpPost]
        public ActionResult Update(LotModel model)
        {
            var lotId = int.Parse(TempData["id"].ToString());

            var lot = _lotRepository.GetLotById(lotId);


            lot.Description = model.Description;

            lot.Title = model.Title;


            _lotRepository.SaveLot(lot);

            return RedirectToAction("Profile", "Lot", new { lot.Id });
        }

        #endregion


        #region My

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult My(int page = 1)
        {
            long userId = _userRepository.GetUserIdByEmail(User.Identity.Name);

            LotsListViewModel model = new LotsListViewModel
            {
                Lots = _lotRepository.Lots
                    .Where(p => p.UserId == userId)
                    .OrderBy(lot => lot.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new LotPageInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _lotRepository.Lots
                        .Count(p => p.UserId == userId)
                }
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult MyWins(int page = 1)
        {
            long userId = _userRepository.GetUserIdByEmail(User.Identity.Name);

            var lots = _lotRepository.Lots
                .Where(p => p.Bids.FirstOrDefault(b => b.UserId == userId).Cost == p.GoldPrice)
                .Where(p => p.IsEnded)
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            LotsListViewModel model = new LotsListViewModel
            {
                Lots = lots,
                PagingInfo = new LotPageInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lots.Count()
                }
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult MyBids(int page = 1)
        {
            long userId = _userRepository.GetUserIdByEmail(User.Identity.Name);

            var bids = _bidRepository.Bids.Where(p => p.UserId == userId)
                                          .OrderBy(p => p.LotId);

            var lots = bids.Select(b => b.Lot);

            BidsListViewModel model = new BidsListViewModel
            {
                Lots = lots
                    .OrderBy(lot => lot.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new LotPageInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lots.Count()
                },
                Bids = bids

            };

            return View(model);
        }
        #endregion


        #region Image

        public FileResult GetImage(int id)
        {
            var lot = _lotRepository.Lots.FirstOrDefault(p => p.Id == id);

            var photo = lot?.Photo;

            if (photo != null)
                return File(photo, "image/png");

            var path = Server.MapPath(lot.IsEnded
                ? "~/Content/DefaultImages/Sold.jpg"
                : "~/Content/DefaultImages/Lot.png");

            return File(path, "image/png");
        }

        [HttpPost]
        public ActionResult UploadImage(long lotId, HttpPostedFileBase image)
        {
            if (image == null)
                return RedirectToAction("Create");

            var lot = _lotRepository.GetLotById(lotId);

            var photo = new byte[image.ContentLength];

            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                photo = binaryReader.ReadBytes(image.ContentLength);
            }

            lot.Photo = photo;

            _lotRepository.SaveLot(lot);

            return RedirectToAction("Profile", "Lot", new { lotId });
        }

        #endregion


        #region Profile

        public ViewResult Profile(long id)
        {
            Lot lot = _lotRepository.Lots.FirstOrDefault(p => p.Id == id);

            _lotRepository.IncViewCount(id);

            ViewBag.sessionUserId = _userRepository.GetUserIdByEmail(User.Identity.Name);

            return View(lot);
        }

        #endregion


        #region Bid On and buy

        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult BidOn(BidOnViewModel mdl)
        {
            if (mdl.Cost < mdl.CurrentPrice || (mdl.GoldPrice != null && mdl.Cost > mdl.GoldPrice))
            {
                ModelState.AddModelError("Cost", "Incorrect input");
            }

            if (ModelState.IsValid)
            {

                Lot lot = _lotRepository.Lots.FirstOrDefault(p => p.Id == mdl.LotId);

                DateTime dt = DateTime.Now;

                long userId = _userRepository.GetUserIdByEmail(User.Identity.Name);


                Bid bid = new Bid()
                {
                    Cost = mdl.Cost,
                    UserId = userId,
                    User = _userRepository.GetUserByEmail(User.Identity.Name),
                    DateTime = dt,
                    LotId = mdl.LotId,
                    Lot = _lotRepository.GetLotById(userId)
                };

                lot.CurrentPrice = mdl.Cost;

                _bidRepository.SaveBid(bid);

                _lotRepository.SaveLot(lot);

                ViewBag.IsValid = true;
            }

            ViewBag.IsValid = false;

            return RedirectToAction("Profile", new { Id = mdl.LotId });
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult Buy(long id)
        {
            Lot lot = _lotRepository.Lots.FirstOrDefault(p => p.Id == id);

            if (lot?.GoldPrice != null)
            {
                DateTime dt = DateTime.Now;

                long userId = _userRepository.GetUserIdByEmail(User.Identity.Name);


                Bid bid = new Bid()
                {
                    Cost = lot.GoldPrice.Value,
                    UserId = userId,
                    User = _userRepository.GetUserByEmail(User.Identity.Name),
                    DateTime = dt,
                    LotId = id,
                    Lot = _lotRepository.GetLotById(userId)
                };


                lot.CurrentPrice = lot.GoldPrice.Value;

                lot.IsEnded = true;


                _bidRepository.SaveBid(bid);

                _lotRepository.SaveLot(lot);

                return RedirectToAction("Profile", "Lot", new { Id = lot.Id });
            }

            return RedirectToAction("List", "Lot");
        }

        #endregion

        #endregion
    }
}