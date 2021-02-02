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
    public class WebsitesController : Controller
    {
        private WebsiteRepository db = null;

        public WebsitesController()
        {
            db = new WebsiteRepository();
        }

        // GET: Admin/Websites
        public ActionResult Index()
        {
            return View(db.SelectAll());
        }

        // GET: Admin/Websites/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.SelectByID(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // GET: Admin/Websites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Websites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Name,Syntax")] Website website)
        {
            if (ModelState.IsValid)
            {
                website.ID = Guid.NewGuid().ToString().Substring(0, 10);
                db.Insert(website);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(website);
        }

        // GET: Admin/Websites/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.SelectByID(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // POST: Admin/Websites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Name,Syntax")] Website website)
        {
            if (ModelState.IsValid)
            {
                db.Update(website);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(website);
        }

        // GET: Admin/Websites/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.SelectByID(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // POST: Admin/Websites/Delete/5
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
