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

namespace BlogApp.Areas.Admin.Controllers
{
    public class RolesController : Controller
    {
        private GenericRepository<Role> repository = null;

        public RolesController()
        {
            this.repository = new GenericRepository<Role>();
        }

        // GET: Admin/Roles
        public ActionResult Index()
        {
            return View(repository.SelectAll());
        }

        // GET: Admin/Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = repository.SelectByID(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Admin/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(role);
                repository.Save();

                using (LogRepository log = new LogRepository())
                {
                    var logModel = new Log();
                    logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                    logModel.PubDate = DateTime.Now;

                    using (AccountRepository account = new AccountRepository())
                    {
                        var user = account.SelectByUserName(User.Identity.Name);
                        logModel.AccountID = user.ID;
                        logModel.Content = String.Format("{0} đã THÊM vai trò {1}", user.Fullname, role.Name);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Admin/Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = repository.SelectByID(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                repository.Update(role);
                repository.Save();

                using (LogRepository log = new LogRepository())
                {
                    var logModel = new Log();
                    logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                    logModel.PubDate = DateTime.Now;

                    using (AccountRepository account = new AccountRepository())
                    {
                        var user = account.SelectByUserName(User.Identity.Name);
                        logModel.AccountID = user.ID;
                        logModel.Content = String.Format("{0} đã SỬA vai trò {1} thành vai trò {2}", user.Fullname, Request["oldName"], role.Name);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Admin/Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = repository.SelectByID(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var role = repository.SelectByID(id);

            repository.Delete(id);
            repository.Save();

            using (LogRepository log = new LogRepository())
            {
                var logModel = new Log();
                logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                logModel.PubDate = DateTime.Now;

                using (AccountRepository account = new AccountRepository())
                {
                    var user = account.SelectByUserName(User.Identity.Name);
                    logModel.AccountID = user.ID;
                    logModel.Content = String.Format("{0} đã XÓA vai trò {1}", user.Fullname, role.Name);
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
    }
}
