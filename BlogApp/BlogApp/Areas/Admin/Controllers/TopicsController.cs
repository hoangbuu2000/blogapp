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
    public class TopicsController : Controller
    {
        private TopicRepository repository = null;

        public TopicsController()
        {
            this.repository = new TopicRepository();
        }

        // GET: Admin/Topics
        public ActionResult Index()
        {
            return View(repository.SelectAll());
        }

        // GET: Admin/Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = repository.SelectByID(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            var posts = repository.GetPostByTopicID(id);
            ViewBag.SoBaiViet = posts.Count();
            return View(topic);
        }

        // GET: Admin/Topics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(topic);
                repository.Save();

                using(LogRepository log = new LogRepository())
                {
                    var logModel = new Log();
                    logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                    logModel.PubDate = DateTime.Now;

                    using (AccountRepository account = new AccountRepository())
                    {
                        var user = account.SelectByUserName(User.Identity.Name);
                        logModel.AccountID = user.ID;
                        logModel.Content = String.Format("{0} đã THÊM chủ đề {1}", user.Fullname, topic.Name);
                    }

                    log.Insert(logModel);
                    log.Save();
                }
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: Admin/Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = repository.SelectByID(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                repository.Update(topic);
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
                        logModel.Content = String.Format("{0} đã SỬA chủ đề {1} thành chủ đề {2}", user.Fullname, Request["oldName"], topic.Name);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Admin/Topics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = repository.SelectByID(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            var posts = repository.GetPostByTopicID(id);
            ViewBag.SoBaiViet = posts.Count();
            return View(topic);
        }

        // POST: Admin/Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var topic = repository.SelectByID(id);

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
                    logModel.Content = String.Format("{0} đã XÓA chủ đề {1}", user.Fullname, topic.Name);
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
