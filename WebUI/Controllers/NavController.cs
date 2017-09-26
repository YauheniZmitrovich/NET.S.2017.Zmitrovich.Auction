using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly ICategoryRepository _repository;

        public NavController(ICategoryRepository rep)
        {
            _repository = rep;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repository.Categories
                .Where(c => c.SubCategory == null)
                .OrderBy(c => c.Id)
                .Select(c => c.Name);

            return PartialView(categories);
        }
    }
}
