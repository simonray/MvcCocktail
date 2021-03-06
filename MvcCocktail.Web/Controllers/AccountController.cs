﻿using MvcCocktail.Domain.Models;
using MvcCocktail.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcCocktail.Web.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous, Route("~/Login")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new AccountLoginMapper().ToView());
        }

        [AllowAnonymous, Route("~/Login")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountLoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (await Security.LoginAsync(this.Request, model.Email, model.Password, model.RememberMe) == LoginState.Success)
                {
                    return Goto("Index", "Home", returnUrl);
                }
            }
            return View(model);
        }

        [Authorize, AjaxOnly, Route("~/Logout")]
        public PartialViewResult Logout()
        {
            return PartialView("_Logout", new AccountLogoutMapper().ToView(AuthHelper.User));
        }

        [Authorize, AjaxOnly, Route("~/Logout")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout(AccountLogoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                Security.Logout(this.Request);
            }
            return Goto("Index", "Home");
        }

        [AllowAnonymous, AjaxOnly, Route("~/Register")]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_Register", new AccountRegisterMapper().ToView());
        }

        [AllowAnonymous, AjaxOnly, Route("~/Register")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountRegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                await SendRegistrationEmailAsync(model, await Application.CreateUserAsync(new AccountRegisterMapper().ToModel(model)));
                return Goto("Index", "Home", returnUrl);
            }
            return PartialView("_Register", model);
        }

        [Authorize, AjaxOnly, Route("~/Manage")]
        public ActionResult Manage()
        {
            return PartialView("_Manage", new AccountProfileMapper().ToView(AuthHelper.User));
        }

        [Authorize, AjaxOnly, Route("~/Manage")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(AccountProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                await Application.UpdateUserAsync(AuthHelper.User, new AccountProfileMapper().ToModel(model, AuthHelper.User));
                return Goto("Index", "Home");
            }
            return PartialView("_Manage", model);
        }

        [AllowAnonymous, AjaxOnly, Route("~/Reset")]
        public ActionResult Reset(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_Reset", new AccountForgotMapper().ToView());
        }

        [AllowAnonymous, AjaxOnly, Route("~/Reset")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Reset(AccountForgotViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser findUser = await Application.GetUserAsync(model.Email);
                if (findUser != null)
                    await SendPasswordResetEmailAsync(findUser);
            }
            return Goto("Index", "Home");
        }

        private Task SendRegistrationEmailAsync(AccountRegisterViewModel model, AppUser newUser)
        {
            return Task.FromResult(true); //TODO: Implement
        }

        private Task SendPasswordResetEmailAsync(AppUser findUser)
        {
            return Task.FromResult(true); //TODO: Implement
        }
    }
}
