using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Areas.Admin.Data
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, 
            string src, string alt, string height, string style)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("height", height);
            builder.MergeAttribute("style", style);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}