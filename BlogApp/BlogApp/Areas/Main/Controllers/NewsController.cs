using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Areas.Admin.Infrastructure.Concrete;
using BlogApp.Areas.Admin.Data;
using BlogApp.Areas.Main.Data;
using PagedList;

namespace BlogApp.Areas.Main.Controllers
{
    public class NewsController : Controller
    {
        private NewsRepository db = null;

        public NewsController()
        {
            db = new NewsRepository();
        }

        // GET: Main/News
        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            int pageSize = 10;

            int pageNumber = (page ?? 1);

            IEnumerable<News> news = db.SelectDESCNews();

            ViewBag.Topic = "RSS";

            ViewBag.totalPage = (int)Math.Ceiling(news.Count() / (float)pageSize);

            return View(news.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(string id)
        {
            NewsViewModel model = new NewsViewModel();
            model.News = db.SelectByID(id);
            model.Prev_News = db.GetPrevious(model.News);
            model.Next_News = db.GetNext(model.News);
            model.Topic = "RSS";

            return View(model);
        }

        public PartialViewResult NewsContent(NewsViewModel model)
        {
            return PartialView("_NewsContent", model);
        }
    }
}