using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Concrete.Repositories;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class LotController : Controller
    {
        private readonly ILotRepository _repository;

        private readonly int _pageSize = 4;//TODO:Replace with ajax

        public LotController(ILotRepository lotRepository)
        {
            this._repository = lotRepository;
        }

        public ViewResult List(int page = 1)
        {
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = _repository.Lots
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * _pageSize)
                    .Take(_pageSize),
                PagingInfo = new LotPageInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.Lots.Count()
                }
            };

            return View(model);
        }
    }
}