using AutoMapper;
using MvcCocktail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCocktail.Web.Controllers
{
    public class AccountLoginMapper : ViewMapper<AccountLoginViewModel, AppUser>
    {
    }

    public class AccountLogoutMapper : ViewMapper<AccountLogoutViewModel, AppUser>
    {
    }
    
    public class AccountRegisterMapper : ViewMapper<AccountRegisterViewModel, AppUser>
    {
        protected override IMappingExpression<AccountRegisterViewModel, AppUser> DefineModelMap()
        {
            return base.DefineModelMap()
                .AfterMap((src, dest) =>
                {
                    dest.SetPassword(src.Password);
                });
        }
    }

    public class AccountForgotMapper : ViewMapper<AccountForgotViewModel, AppUser>
    {

    }

    public class AccountProfileMapper : ViewMapper<AccountProfileViewModel, AppUser>
    {
        protected override IMappingExpression<AccountProfileViewModel, AppUser> DefineModelMap()
        {
            return base.DefineModelMap()
                .Ignore(required => required.Email)
                .AfterMap((src, dest) =>
                {
                    if (string.IsNullOrEmpty(src.Password) == false)
                        dest.SetPassword(src.Password);
                });
        }
    }
}