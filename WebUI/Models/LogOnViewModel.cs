using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "E-mail:")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Incorrect e-mail.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [DataType(DataType.Password, ErrorMessage = "Incorrect password.")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}
