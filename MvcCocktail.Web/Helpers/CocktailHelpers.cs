using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class BootstrapTagHelpers
    {
        public static MvcHtmlString TitleTags(this HtmlHelper htmlHelper, string title, params string[] tags)
        {
            var div = new TagBuilder("div");

            var heading = new TagBuilder("h1");
            heading.InnerHtml = title;
            div.InnerHtml += heading;
           
            foreach(string tag in tags)
            {
                var anchor = htmlHelper.Tag(tag, new { @class = "tag label label-default", rel = "tag" });
                div.InnerHtml += anchor + " ";
            }
            return MvcHtmlString.Create(div.ToString());
        }

        public static MvcHtmlString NewTag(this HtmlHelper htmlHelper)
        {
            return SmallTag(htmlHelper, "New");
        }

        public static MvcHtmlString SoonTag(this HtmlHelper htmlHelper)
        {
            return SmallTag(htmlHelper, "Soon", new { @class = "label label-warning" });
        }

        public static MvcHtmlString UpdatedTag(this HtmlHelper htmlHelper)
        {
            return SmallTag(htmlHelper, "Updated", new { @class = "label label-info" });
        }

        public static MvcHtmlString SmallTag(this HtmlHelper htmlHelper, string tag, object htmlAttributes = null)
        {
            return MvcHtmlString.Create(string.Format("<small>{0}</small>", Tag(htmlHelper, tag, htmlAttributes)));
        }

        public static MvcHtmlString Tag(this HtmlHelper htmlHelper, string tag, object htmlAttributes = null)
        {
            if (htmlAttributes == null)
                htmlAttributes = new { @class = "label label-success" };

            string stringAttributes = string.Empty;
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(htmlAttributes))
            {
                stringAttributes += string.Format("{0}=\"{1}\" ", property.Name.Replace('_', '-'), property.GetValue(htmlAttributes));
            }
            htmlAttributes = stringAttributes;

            return MvcHtmlString.Create(string.Format("<span {0}>{1}</span>", htmlAttributes, tag));
        }
    }
}