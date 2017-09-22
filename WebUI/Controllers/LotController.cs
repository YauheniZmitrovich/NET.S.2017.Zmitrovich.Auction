using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class LotController : Controller
    {
        private ILotRepository repository;

        public LotController(ILotRepository lotRepository)
        {
            this.repository = lotRepository;
        }

        public ViewResult List()
        {
            return View(repository.Lots);
        }
    }

}