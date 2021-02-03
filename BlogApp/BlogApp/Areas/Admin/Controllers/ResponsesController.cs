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
    public class ResponsesController : Controller
    {
        private ResponseRepository repository = null;

        public ResponsesController()
        {
            this.repository = new ResponseRepository();
        }

        // GET: Admin/Responses
        public ActionResult Index()
        {
            var model = repository.SelectAllWithNoLazy();
            return View(model);
        }

        // GET: Admin/Responses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = repository.SelectByID(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // GET: Admin/Responses/Create
        public ActionResult Create()
        {
            ViewBag.Post_ID = new SelectList(repository.GetPost(), "ID", "Title");
            return View();
        }

        // POST: Admin/Responses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Content,PubDate,Post_ID,Username,Email,Website")] Response response)
        {
            if (ModelState.IsValid)
            {
                response.ID = Guid.NewGuid().ToString().Substring(0, 10);
                response.PubDate = DateTime.Now;
                repository.Insert(response);
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
                        logModel.Content = String.Format("{0} đã THÊM phản hồi có mã {1} cho bài viết {2}", user.Fullname, response.ID, response.Post_ID);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                return RedirectToAction("Index");
            }
            ViewBag.Post_ID = new SelectList(repository.GetPost(), "ID", "Title", response.Post_ID);
            return View(response);
        }

        // GET: Admin/Responses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = repository.SelectByID(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            ViewBag.Post_ID = new SelectList(repository.GetPost(), "ID", "Title", response.Post_ID);
            return View(response);
        }

        // POST: Admin/Responses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Content,PubDate,Post_ID,Username,Email,Website")] Response response)
        {
            if (ModelState.IsValid)
            {
                repository.Update(response);
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
                        logModel.Content = String.Format("{0} đã SỬA phản hồi có mã {1} của bài viết {2}", user.Fullname, response.ID, response.Post_ID);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                return RedirectToAction("Index");
            }
            ViewBag.Post_ID = new SelectList(repository.GetPost(), "ID", "Title", response.Post_ID);
            return View(response);
        }

        // GET: Admin/Responses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = repository.SelectByID(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Admin/Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var res = repository.SelectByID(id);

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
                    logModel.Content = String.Format("{0} đã XÓA phản hồi có mã {1} của bài viết {2}", user.Fullname, res.ID, res.Post_ID);
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
