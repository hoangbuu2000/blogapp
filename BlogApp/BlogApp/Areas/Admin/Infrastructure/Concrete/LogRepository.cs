using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Data;
using System.Data.Entity;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class LogRepository : GenericRepository<Log>, IDisposable
    {
        public IEnumerable<Log> SelectAllWithNoLazy()
        {
            return this.table.Include(l => l.Account).ToList();
        }

        public IEnumerable<Log> SelectDESCLog()
        {
            var result = (from p in table
                          orderby p.PubDate descending
                          select p).Include(l => l.Account).ToList();
            return result;
        }

        public IEnumerable<Account> GetAccount()
        {
            return this.db.Account.ToList();
        }
    }
}