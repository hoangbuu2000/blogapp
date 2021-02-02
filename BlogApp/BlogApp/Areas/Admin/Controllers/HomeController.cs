using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.Username = User.Identity.Name;
            return View();
        }

        public PartialViewResult SideBar(string url)
        {
            switch(url)
            {
                case "Home":
                    ViewBag.HomeActive = "active";
                    break;
                case "Topics":
                    ViewBag.TopicActive = "active";
                    break;
                case "Posts":
                    ViewBag.PostActive = "active";
                    break;
                case "Responses":
                    ViewBag.ResActive = "active";
                    break;
                case "Roles":
                    ViewBag.RoleActive = "active";
                    break;
                case "Accounts":
                    ViewBag.AccActive = "active";
                    break;
                case "News":
                    ViewBag.NewsActive = "active";
                    break;
                case "Websites":
                    ViewBag.WebActive = "active";
                    break;
                case "Logs":
                    ViewBag.LogActive = "active"; ;
                    break;
            }
            return PartialView("SideBar");
        }

        public PartialViewResult NavBar()
        {
            return PartialView("_NavBar");
        }

        public PartialViewResult Footer()
        {
            return PartialView("_Footer");
        }

        public PartialViewResult FixedPlugin()
        {
            return PartialView("_FixedPlugin");
        }
    }
}