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

    public class AccountRegisterViewModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, DataType(DataType.Password), StringLength(255, MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required, Compare("Password")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }

    public class AccountForgotViewModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AccountProfileViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Password), StringLength(255, MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}