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
    public class LogsController : Controller
    {
        private LogRepository db = null;

        public LogsController()
        {
            db = new LogRepository();
        }

        // GET: Admin/Logs
        public ActionResult Index()
        {
            var log = db.SelectAllWithNoLazy();
            return View(log);
        }

        // GET: Admin/Logs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.SelectByID(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // GET: Admin/Logs/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.GetAccount(), "ID", "Username");
            return View();
        }

        // POST: Admin/Logs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Content,PubDate,AccountID")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Insert(log);
                db.Save();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.GetAccount(), "ID", "Username", log.AccountID);
            return View(log);
        }

        // GET: Admin/Logs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.SelectByID(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.GetAccount(), "ID", "Username", log.AccountID);
            return View(log);
        }

        // POST: Admin/Logs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Content,PubDate,AccountID")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Update(log);
                db.Save();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.GetAccount(), "ID", "Username", log.AccountID);
            return View(log);
        }

        // GET: Admin/Logs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.SelectByID(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Admin/Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            db.Delete(id);
            db.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
