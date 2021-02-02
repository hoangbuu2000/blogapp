using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Data;
using System.Data.Entity;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class NewsRepository : GenericRepository<News>
    {
        public IEnumerable<Website> GetWebsite()
        {
            return this.db.Website.ToList();
        }

        public IEnumerable<News> SelectAllWithNoLazy()
        {
            return this.table.Include(n => n.Website).ToList();
        }

        public Website SelectWebsiteByName(string name)
        {
            return this.db.Website.FirstOrDefault(w => w.Name.Contains(name));
        }

        public IEnumerable<News> SelectDESCNews()
        {
            var result = (from p in table
                          orderby p.PubDate descending
                          select p).Include(n => n.Website).ToList();
            return result;
        }

        public News GetPrevious(News n)
        {
            News result;
            try
            {
                result = (from x in this.table
                          where x.PubDate < n.PubDate
                          orderby x.PubDate descending
                          select x).First();
            }
            catch
            {
                return null;
            }
            return result;
        }

        public News GetNext(News n)
        {
            News result;
            try
            {
                result = (from x in this.table
                          where x.PubDate > n.PubDate
                          orderby x.PubDate ascending
                          select x).First();
            }
            catch
            {
                return null;
            }
            return result;
        }
    }
}