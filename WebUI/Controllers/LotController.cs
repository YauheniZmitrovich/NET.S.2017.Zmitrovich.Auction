using System;
using System.Collections.Generic;
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
        private readonly ILotRepository _lotRepository;

        private readonly IBidRepository _bidRepository;


        public int PageSize { get; set; } = 4;

        public LotController(ILotRepository lotRepository, IBidRepository bidRepository)
        {
            this._lotRepository = lotRepository;

            this._bidRepository = bidRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = _lotRepository.Lots
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
                        _lotRepository.Lots.Count(lot => lot.Category.Name == category)
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public FileResult GetImage(int id)
        {
            var photo = _lotRepository.Lots.FirstOrDefault(p => p.Id == id)?.Photos.FirstOrDefault();

            if (photo != null)
                return File(photo.Content, "image/png");

            var path = Server.MapPath("~/Content/DefaultImages/Lot.png");

            return File(path, "image/png");
        }

        public ViewResult Profile(long id)
        {
            Lot lot = _lotRepository.Lots.FirstOrDefault(p => p.Id == id);

            _lotRepository.IncViewCount(id);

            return View(lot);
        }

        //[HttpGet]
        //[Authorize(Roles = "user")]
        //public ActionResult Buy()
        //{
        //    return PartialView("_BuyLot");
        //}

        [Authorize(Roles = "user")]
        public ActionResult Buy(long id)
        {
            Lot lot = _lotRepository.Lots.FirstOrDefault(p => p.Id == id);

            DateTime dt = DateTime.Now;

            Bid bid = new Bid()
            {
                Cost = lot.GoldPrice.Value,
                UserId = (long)Membership.GetUser().ProviderUserKey,
                DateTime = dt,
                LotId = id
            };

            lot.IsEnded = true;

            lot.EndOfTranding = dt;

            lot.CurrentPrice = lot.GoldPrice.Value;

            _bidRepository.SaveBid(bid);

            _lotRepository.SaveLot(lot);

            return RedirectToAction("Profile", new { Id = id });
        }
    }
}