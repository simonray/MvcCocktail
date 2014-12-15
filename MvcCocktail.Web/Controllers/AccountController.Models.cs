using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCocktail.Web.Controllers
{
    public class AccountLoginViewModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), StringLength(255, MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class AccountLogoutViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }

}