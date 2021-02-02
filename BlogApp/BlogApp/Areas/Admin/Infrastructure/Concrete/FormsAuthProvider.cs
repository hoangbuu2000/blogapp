using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BlogApp.Areas.Admin.Infrastructure.Abstract;
using BlogApp.Areas.Admin.Data;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        private blogEntities db = new blogEntities();
        public bool Authenticate(string username, string password)
        {
            var acc = db.Account.FirstOrDefault(a => a.Username.Equals(username) &&
                                                a.Password.Equals(password) && a.Role.Name.Equals("admin"));
            if (acc != null)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return true;
            }
            return false;
        }
    }
}