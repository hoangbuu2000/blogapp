﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Areas.Admin.Data
{
    public class RSSFeed
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
    }
}