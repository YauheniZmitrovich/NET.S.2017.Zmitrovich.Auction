    using System;
using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "Enter your username.")]
        [Display(Name = "Username:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your password.")]
        [DataType(DataType.Password, ErrorMessage = "Incorrect password.")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}