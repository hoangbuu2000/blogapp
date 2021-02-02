using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Data;
using System.Data.Entity;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class ResponseRepository : GenericRepository<Response>
    {
        public IEnumerable<Response> SelectAllWithNoLazy()
        {
            return this.table.Include(r => r.Post).ToList();
        }

        public IEnumerable<Post> GetPost()
        {
            return this.db.Post.ToList();
        }
    }
}