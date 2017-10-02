using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILotRepository _lotRepository;

        public HomeController(ILotRepository lotRepository)
        {
            _lotRepository = lotRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LotSearch(string name)
        {
            var lots = _lotRepository.Lots.Where(a => a.Title.Contains(name)).ToList();

            if (lots.Count <= 0)
            {
                return HttpNotFound();
            }

            return PartialView("LotSearch",lots);
        }
    }
}