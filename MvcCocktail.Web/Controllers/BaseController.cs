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

        public BaseController() { }

        public BaseController(IApplicationServices services, ISecurityServices securityServices)
        {
            Application = services;
            Security = securityServices;
        }
    }
}
