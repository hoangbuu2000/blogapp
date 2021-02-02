using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Data;

namespace BlogApp.Areas.Main.Data
{
    public class NewsViewModel
    {
        public News News { get; set; }
        public News Prev_News { get; set; }
        public News Next_News { get; set; }
        public String Topic { get; set; }
    }
}