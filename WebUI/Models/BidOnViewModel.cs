using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Entities;
using WebUI.Infrastructure;

namespace WebUI.Models
{
    public class BidOnViewModel
    {
        public long LotId { get; set; }

        //[RegularExpression(@"[0-9.]",
        //    ErrorMessage = "Incorrect input")]
        public decimal Cost { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal? GoldPrice { get; set; }
    }
}