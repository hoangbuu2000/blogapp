using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Areas.Admin.Data;
using BlogApp.Areas.Admin.Infrastructure.Concrete;
using BlogApp.Areas.Main.Data;
using PagedList;

namespace BlogApp.Areas.Main.Controllers
{
    public class BlogController : Controller
    {
        private PostRepository db = null;
        private ResponseRepository resdb = null;

        public BlogController()
        {
            this.db = new PostRepository();
        }

        // GET: Main/Blog
        public ActionResult Index(int? topicID, int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            int pageSize = 10;

            int pageNumber = (page ?? 1);

            IEnumerable<Post> posts = db.SelectDESCPost();

            ViewBag.Topic = "General";

            if (topicID != null)
            {
                Topic topic = db.GetTopic().Where(t => t.ID == topicID).First();
                posts = db.SelectPostInTopic(topic);
                ViewBag.Topic = topic.Name;
                ViewBag.TopicID = topic.ID;
            }

            ViewBag.totalPage = (int)Math.Ceiling(posts.Count() / (float)pageSize);

            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(string id)
        {
            BlogViewModel model = new BlogViewModel();
            model.Blog = db.SelectByID(id);
            model.Prev_Blog = db.GetPrevious(model.Blog);
            model.Next_Blog = db.GetNext(model.Blog);
            model.Topic = db.GetTopic().Where(t => t.ID == model.Blog.Topic_ID).First();
            model.RelatedPost = db.SelectPostInTopic(model.Topic, model.Blog, 2);
            model.Responses = db.SelectResponse(model.Blog);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Details(string CurrentBlogID, BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                Response response = new Response();
                resdb = new ResponseRepository();

                response.ID = Guid.NewGuid().ToString().Substring(0, 10);
                response.PubDate = DateTime.Now;
                response.Post_ID = CurrentBlogID.Trim();
                response.Content = model.Content;
                response.Username = model.Username;
                response.Email = model.Email;
                response.Website = model.Website;

                resdb.Insert(response);
                resdb.Save();
            }
            return RedirectToAction("Details", new { id = CurrentBlogID.Trim() });
        }

        public PartialViewResult BlogContent(BlogViewModel model)
        {
            return PartialView("_BlogContent", model);
        }
    }
}