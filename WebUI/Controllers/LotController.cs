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

        public int PageSize { get; set; } = 4; //TODO:Replace with ajax

        public LotController(ILotRepository lotRepository)
        {
            this._repository = lotRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            LotsListViewModel model = new LotsListViewModel
            {
                Lots = _repository.Lots
                    .Where(p => category == null || p.Category.Name == category)
                    .OrderBy(lot => lot.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new LotPageInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        _repository.Lots.Count() :
                        _repository.Lots.Where(lot => lot.Category.Name == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }
    }
}