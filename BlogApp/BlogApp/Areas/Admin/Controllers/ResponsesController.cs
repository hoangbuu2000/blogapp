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
            repository.Delete(id);
            repository.Save();
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
