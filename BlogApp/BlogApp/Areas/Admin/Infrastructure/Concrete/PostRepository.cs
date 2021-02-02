using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BlogApp.Areas.Admin.Data;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class PostRepository : GenericRepository<Post>
    {
        public IEnumerable<Post> SelectAllWithNoLazy()
        {
            return this.table.Include(p => p.Account).Include(p => p.Topic).ToList();
        }

        public IEnumerable<Account> GetAccount()
        {
            return this.db.Account.ToList();
        }

        public IEnumerable<Topic> GetTopic()
        {
            return this.db.Topic.ToList();
        }

        public Account GetAccountByUserName(string username)
        {
            return this.db.Account.Where(x => x.Username.Equals(username)).First();
        }

        public Post GetPrevious(Post p)
        {
            Post result;
            try
            {
                result = (from x in this.table
                          where x.PubDate < p.PubDate
                          orderby x.PubDate descending
                          select x).First();
            }
            catch
            {
                return null;
            }
            return result;
        }

        public Post GetNext(Post p)
        {
            Post result;
            try
            {
                result = (from x in this.table
                          where x.PubDate > p.PubDate
                          orderby x.PubDate ascending
                          select x).First();
            }
            catch
            {
                return null;
            }
            return result;
        }

        public IEnumerable<Post> SelectPinPost()
        {
            var result = (from p in table
                          where p.IsPin == 1
                          orderby p.PubDate descending
                          select p).Take(3).ToList();
            return result;
        }

        public IEnumerable<Post> SelectDESCPost()
        {
            var result = (from p in table
                          orderby p.PubDate descending
                          select p).ToList();
            return result;
        }

        public IEnumerable<Post> SelectPostInTopic(Topic topic, Post post = null, int num = 0)
        {
            IEnumerable<Post> result;
            if (num != 0)
            {
                if (post == null)
                {
                    result = (from p in table
                              join t in db.Topic on p.Topic_ID equals t.ID
                              where t.ID == topic.ID
                              orderby p.PubDate descending
                              select p).Take(num).ToList();
                }
                else
                {
                    result = (from p in table
                              join t in db.Topic on p.Topic_ID equals t.ID
                              where t.ID == topic.ID && p.ID != post.ID
                              orderby p.PubDate descending
                              select p).Take(num).ToList();
                }
            }
            else
            {
                if (post == null)
                {
                    result = (from p in table
                              join t in db.Topic on p.Topic_ID equals t.ID
                              where t.ID == topic.ID
                              orderby p.PubDate descending
                              select p).ToList();
                }
                else
                {
                    result = (from p in table
                              join t in db.Topic on p.Topic_ID equals t.ID
                              where t.ID == topic.ID && p.ID != post.ID
                              orderby p.PubDate descending
                              select p).ToList();
                }
            }

            return result;
        }

        public IEnumerable<Response> SelectResponse(Post p)
        {
            IEnumerable<Response> result;

            result = (from r in db.Response
                      where r.Post_ID.Equals(p.ID)
                      orderby r.PubDate descending
                      select r).ToList();

            return result;
        }
    }
}