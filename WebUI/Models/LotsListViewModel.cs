using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class LotsListViewModel
    { 
        public IEnumerable<Lot> Lots { get; set; }

        public LotPageInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}