using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogApp.Areas.Admin.Data;
using BlogApp.Areas.Admin.Infrastructure.Concrete;

namespace BlogApp.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private PostRepository repository = null;

        public PostsController()
        {
            this.repository = new PostRepository();
        }

        // GET: Admin/Posts
        public ActionResult Index()
        {
            var model = repository.SelectAllWithNoLazy();
            return View(model);
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = repository.SelectByID(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.Account_ID = new SelectList(repository.GetAccount(), "ID", "Username");
            ViewBag.Topic_ID = new SelectList(repository.GetTopic(), "ID", "Name");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Title,Content,Image,PubDate,Topic_ID,Account_ID,IsPin")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.ID = Guid.NewGuid().ToString().Substring(0, 10);
                post.PubDate = DateTime.Now;
                post.Account_ID = repository.GetAccountByUserName(User.Identity.Name).ID;
                repository.Insert(post);
                repository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Account_ID = new SelectList(repository.GetAccount(), "ID", "Username", post.Account_ID);
            ViewBag.Topic_ID = new SelectList(repository.GetTopic(), "ID", "Name", post.Topic_ID);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = repository.SelectByID(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account_ID = new SelectList(repository.GetAccount(), "ID", "Username", post.Account_ID);
            ViewBag.Topic_ID = new SelectList(repository.GetTopic(), "ID", "Name", post.Topic_ID);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,Image,PubDate,Topic_ID,Account_ID,IsPin")] Post post)
        {
            if (ModelState.IsValid)
            {
                repository.Update(post);
                repository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Account_ID = new SelectList(repository.GetAccount(), "ID", "Username", post.Account_ID);
            ViewBag.Topic_ID = new SelectList(repository.GetTopic(), "ID", "Name", post.Topic_ID);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = repository.SelectByID(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
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
