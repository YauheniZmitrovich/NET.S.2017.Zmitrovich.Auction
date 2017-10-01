using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class LotModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [StringLength(30)]
        [Required(ErrorMessage = "The field can not be empty")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Current price")]
        [Required(ErrorMessage = "The field can not be empty")]
        public decimal CurrentPrice { get; set; }

        [Display(Name = "Gold price")]
        public decimal GoldPrice { get; set; }

        [Display(Name = "Name of category")]
        [Required(ErrorMessage = "The field can not be empty")]
        public string Category { get; set; }

        public byte[] Image { get; set; }
    }
}