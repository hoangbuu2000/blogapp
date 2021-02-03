using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BlogApp.Areas.Admin.Data;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class AccountRepository : GenericRepository<Account>, IDisposable
    {
        public IEnumerable<Role> GetRole()
        {
            return this.db.Role.ToList();
        }

        public IEnumerable<Account> SelectAllWithNoLazy()
        {
            return this.table.Include(a => a.Role).ToList();
        }

        public Account SelectByUserName(string username)
        {
            return this.table.Where(u => u.Username.Equals(username)).FirstOrDefault();
        }

        public Account SelectByEmail(string email)
        {
            return this.table.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }
    }
}