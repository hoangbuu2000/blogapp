using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Data;

namespace BlogApp.Areas.Main.Data
{
    public class TopicViewModel
    {
        public List<Topic> Topics { get; set; }
        public Dictionary<Topic, List<Post>> PostInTopic { get; set; }
    }
}