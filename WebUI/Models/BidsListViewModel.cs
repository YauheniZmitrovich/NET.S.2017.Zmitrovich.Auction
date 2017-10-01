using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class BidsListViewModel
    {
        public IQueryable<Lot> Lots { get; set; }

        public LotPageInfo PagingInfo { get; set; }

        public IQueryable<Bid> Bids { get; set; }
    }
}