using MvcCocktail.Domain.Models;
using MvcCocktail.Services;
using MvcCocktail.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcCocktail.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IApplicationServices Application { get; private set; }
        protected ISecurityServices Security { get; private set; }
        public Settings Settings { get { return Application.GetSettingsCached(); } }

        public BaseController()
            : this(ApplicationServices.Instance, SecurityServices.Instance) { }

        public BaseController(IApplicationServices services, ISecurityServices securityServices)
        {
            Application = services;
            Security = securityServices;
        }

        public ActionResult Goto(string action, string controller, string returnUrl = null)
        {
            if (Request.IsAjaxRequest())
            {
                if (string.IsNullOrEmpty(returnUrl))
                    returnUrl = Url.Action(action, controller);
                return JavaScript(string.Format("window.location.href='{0}'", returnUrl));
            }
            else
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction(action, controller);
            }
        }
    }
}
