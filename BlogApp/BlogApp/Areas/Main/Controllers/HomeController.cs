using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Areas.Admin.Data;
using PagedList;
using BlogApp.Areas.Admin.Infrastructure.Concrete;
using BlogApp.Areas.Main.Data;

namespace BlogApp.Areas.Main.Controllers
{
    public class HomeController : Controller
    {
        private PostRepository db = null;

        public HomeController()
        {
            db = new PostRepository();
        }

        // GET: Home
        public ActionResult Index()
        {
            var topPosts = db.SelectPinPost();

            return View(topPosts);
        }

        public PartialViewResult RecentBlog(int? page)
        {
            if (page == null)
                page = 1;

            var posts = db.SelectDESCPost();

            int pageSize = 10;

            int pageNumber = (page ?? 1);

            ViewBag.totalPage = (int)Math.Ceiling(posts.Count() / (float)pageSize);

            return PartialView("_RecentBlog", posts.ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult Header()
        {
            TopicViewModel model = new TopicViewModel();

            model.Topics = db.GetTopic() as List<Topic>;
            model.PostInTopic = new Dictionary<Topic, List<Post>>();

            foreach(Topic topic in model.Topics)
            {
                model.PostInTopic.Add(topic, db.SelectPostInTopic(topic, null, 3) as List<Post>);
            }

            ViewData["url"] = Url.Content("~/") + "Images";
            return PartialView("_Header", model);
        }
    }
}