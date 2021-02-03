using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogApp.Areas.Admin.Data;
using BlogApp.Areas.Admin.Infrastructure.Concrete;
using System.Web.Security;

namespace BlogApp.Areas.Admin.Controllers
{
    public class AccountsController : Controller
    {
        private AccountRepository repository = null;
        private FormsAuthProvider auth = null;

        public AccountsController()
        {
            this.repository = new AccountRepository();
            this.auth = new FormsAuthProvider();
        }

        // GET: Admin/Accounts
        public ActionResult Index()
        {
            var model = repository.SelectAllWithNoLazy();
            return View(model);
        }

        // GET: Admin/Accounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = repository.SelectByID(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Admin/Accounts/Create
        public ActionResult Create()
        {
            ViewBag.Role_ID = new SelectList(repository.GetRole(), "ID", "Name");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password,Email,Fullname,Active,Role_ID")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.ID = Guid.NewGuid().ToString().Substring(0, 10);
                account.AccessDate = DateTime.Now;
                repository.Insert(account);
                repository.Save();

                using (LogRepository log = new LogRepository())
                {
                    var logModel = new Log();
                    logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                    logModel.PubDate = DateTime.Now;

                    using (AccountRepository acc = new AccountRepository())
                    {
                        var user = acc.SelectByUserName(User.Identity.Name);
                        logModel.AccountID = user.ID;
                        logModel.Content = String.Format("{0} đã THÊM tài khoản {1} thuộc vai trò {2}", user.Fullname, account.Username, account.Role_ID);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Role_ID = new SelectList(repository.GetRole(), "ID", "Name", account.Role_ID);
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = repository.SelectByID(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_ID = new SelectList(repository.GetRole(), "ID", "Name", account.Role_ID);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Email,Fullname,Active,AccessDate,Role_ID")] Account account)
        {
            if (ModelState.IsValid)
            {
                repository.Update(account);
                repository.Save();

                using (LogRepository log = new LogRepository())
                {
                    var logModel = new Log();
                    logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                    logModel.PubDate = DateTime.Now;

                    using (AccountRepository acc = new AccountRepository())
                    {
                        var user = acc.SelectByUserName(User.Identity.Name);
                        logModel.AccountID = user.ID;
                        logModel.Content = String.Format("{0} đã SỬA tài khoản {1}", user.Fullname, account.Username);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                return RedirectToAction("Index");
            }
            ViewBag.Role_ID = new SelectList(repository.GetRole(), "ID", "Name", account.Role_ID);
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = repository.SelectByID(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var account = repository.SelectByID(id);

            repository.Delete(id);
            repository.Save();

            using (LogRepository log = new LogRepository())
            {
                var logModel = new Log();
                logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                logModel.PubDate = DateTime.Now;

                using (AccountRepository acc = new AccountRepository())
                {
                    var user = acc.SelectByUserName(User.Identity.Name);
                    logModel.AccountID = user.ID;
                    logModel.Content = String.Format("{0} đã XÓA tài khoản {1}", user.Fullname, account.Username);
                }

                log.Insert(logModel);
                log.Save();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (auth.Authenticate(model.Username, model.Password))
                {

                    using (LogRepository log = new LogRepository())
                    {
                        var logModel = new Log();
                        logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                        logModel.PubDate = DateTime.Now;

                        using (AccountRepository account = new AccountRepository())
                        {
                            var user = account.SelectByUserName(model.Username);
                            logModel.AccountID = user.ID;
                            logModel.Content = String.Format("{0} đã đăng nhập", user.Fullname);
                        }

                        log.Insert(logModel);
                        log.Save();
                    }

                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else
                {
                    ModelState.AddModelError("Login", "Sai tài khoản hoặc mật khẩu!");
                    return View();
                }
            }
            return View();
        }

        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (repository.SelectByUserName(model.Username) != null)
                {
                    ModelState.AddModelError("Username", "Username đã tồn tại");
                }
                if (repository.SelectByEmail(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại");
                }
                if (repository.SelectByUserName(model.Username) == null && repository.SelectByEmail(model.Email) == null)
                {
                    Account account = new Account();
                    account.ID = model.ID;
                    account.Username = model.Username;
                    account.Password = model.Password;
                    account.Fullname = model.Fullname;
                    account.Email = model.Email;
                    account.Active = model.Active;
                    account.AccessDate = model.AccessDate;
                    account.Role_ID = model.Role_ID;
                    repository.Insert(account);
                    repository.Save();
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                return View();
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
