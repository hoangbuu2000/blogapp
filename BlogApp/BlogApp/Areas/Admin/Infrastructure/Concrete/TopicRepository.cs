using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BlogApp.Areas.Admin.Data;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class TopicRepository : GenericRepository<Topic>
    {
        public IEnumerable<Post> GetPostByTopicID(int? id)
        {
            return this.db.Post.Where(m => m.Topic_ID == id).ToList();
        }
    }
}