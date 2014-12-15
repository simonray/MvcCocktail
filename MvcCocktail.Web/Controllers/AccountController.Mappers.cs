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

}