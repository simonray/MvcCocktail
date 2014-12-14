using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class ActionLinkHelpers
    {
        public static string ActionReturnUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action(
                urlHelper.RequestContext.RouteData.Values["action"].ToString(),
                urlHelper.RequestContext.RouteData.Values["controller"].ToString()
                );
        }

        public static MvcHtmlString ActionLinkRaw(this HtmlHelper htmlHelper, string rawHtml, string actionName, string controllerName, object routeValues, object htmlAttributes = null)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = htmlHelper.ActionLink(repID, actionName, controllerName, routeValues, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, rawHtml));
        }

        public static MvcHtmlString ActionLinkRaw(this AjaxHelper ajaxHelper, string rawHtml, string actionName, string controllerName, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            return ActionLinkRaw(ajaxHelper, rawHtml, actionName, controllerName, null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString ActionLinkRaw(this AjaxHelper ajaxHelper, string rawHtml, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, rawHtml));
        }

        public static MvcHtmlString ActionModal(this AjaxHelper ajaxHelper, string link, string actionName, string controller, string modal, object routeValues = null, object htmlAttributes = null)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controller, routeValues,
                new AjaxOptions
                {
                    UpdateTargetId = modal,
                    OnSuccess = string.Format("$('#{0}').modal('show')", modal),
                }, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, link));
        }

        //http://chrisondotnet.com/2012/08/setting-active-link-twitter-bootstrap-navbar-aspnet-mvc/
        public static MvcHtmlString ActionLinkMenu(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var builder = new TagBuilder("li")
            {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString()
            };

            if (controllerName == currentController && actionName == currentAction)
            {
                builder.AddCssClass("active");
            }

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString ActionLinkIcon(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string icon)
        {
            var builder = new TagBuilder("i");
            builder.MergeAttribute("class", icon);
            var lnk = ActionLinkMenu(htmlHelper, "[replaceme] " + linkText, actionName, controllerName).ToHtmlString();
            return new MvcHtmlString(lnk.Replace("[replaceme]", builder.ToString()));
        }
    }
}