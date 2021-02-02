using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Data;

namespace BlogApp.Areas.Main.Data
{
    public class ReplyModelView
    {
        public Post CurrentPost { get; set; }
        public Account CurrentAccount { get; set; }
        public String DisplayName { get; set; }
        public String Email { get; set; }
        public String Website { get; set; }
        public String Content { get; set; }
    }
}