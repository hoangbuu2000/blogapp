using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using BlogApp.Areas.Admin.Data;
using BlogApp.Areas.Admin.Infrastructure.Concrete;

namespace BlogApp.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private NewsRepository db = null;

        public NewsController()
        {
            db = new NewsRepository();
        }

        // GET: Admin/News
        public ActionResult Index()
        {
            var news = db.SelectDESCNews();
            ViewBag.websiteID = new SelectList(db.GetWebsite(), "ID", "Name");
            return View(news);
        }

        [HttpPost]
        public ActionResult Index(string websiteID)
        {
            using(WebsiteRepository webRepo = new WebsiteRepository())
            {
                var website = webRepo.SelectByID(websiteID);
                var RSSURL = website.Name.Substring(website.Name.IndexOf(':') + 1);
                var start = website.Syntax.Substring(0, website.Syntax.IndexOf(','));
                var end = website.Syntax.Substring(website.Syntax.IndexOf(',') + 1);

                WebClient wclient = new WebClient();
                wclient.Encoding = System.Text.Encoding.UTF8;
                string RSSData = wclient.DownloadString(RSSURL);

                XDocument xml = XDocument.Parse(RSSData);
                var RSSFeedData = (from x in xml.Descendants("item")
                                   select new RSSFeed
                                   {
                                       Title = ((string)x.Element("title")),
                                       Link = ((string)x.Element("link")),
                                       Description = ((string)x.Element("description")),
                                       PubDate = ((string)x.Element("pubDate"))
                                   });

                foreach (var item in RSSFeedData)
                {
                    var article = (from a in db.SelectAll()
                                   where a.Title == item.Title
                                   select a).FirstOrDefault<News>();
                    if (article != null)
                    {
                        // neu da ton tai bao bai thi continue sang buoc lap ke tiep
                        continue;
                    }

                    string s = wclient.DownloadString(item.Link);
                    string content = s.Substring(s.IndexOf(start), s.IndexOf(end) - s.IndexOf(start));

                    // luu xuong database
                    var news = new News();
                    news.ID = Guid.NewGuid().ToString().Substring(0, 10);
                    news.Image = "/images/upload/loading.jpg";
                    news.PubDate = DateTime.Now;
                    news.Title = item.Title;
                    news.Content = content;
                    var topic = "rss";
                    news.Topic = topic;
                    news.WebsiteID = website.ID;
                    db.Insert(news);
                    db.Save();
                }

                using (LogRepository log = new LogRepository())
                {
                    var logModel = new Log();
                    logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                    logModel.PubDate = DateTime.Now;

                    using (AccountRepository account = new AccountRepository())
                    {
                        var user = account.SelectByUserName(User.Identity.Name);
                        logModel.AccountID = user.ID;
                        logModel.Content = String.Format("{0} đã sử dụng chức năng lấy tin tự động", user.Fullname);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                ViewBag.websiteID = new SelectList(db.GetWebsite(), "ID", "Name");
                return View(db.SelectDESCNews());
            }
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.SelectByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.WebsiteID = new SelectList(db.GetWebsite(), "ID", "Name", news.WebsiteID);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,Image,PubDate,Topic,WebsiteID")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Update(news);
                db.Save();

                using (LogRepository log = new LogRepository())
                {
                    var logModel = new Log();
                    logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                    logModel.PubDate = DateTime.Now;

                    using (AccountRepository account = new AccountRepository())
                    {
                        var user = account.SelectByUserName(User.Identity.Name);
                        logModel.AccountID = user.ID;
                        logModel.Content = String.Format("{0} đã SỬA tin tức {1}", user.Fullname, news.Title);
                    }

                    log.Insert(logModel);
                    log.Save();
                }

                return RedirectToAction("Index");
            }
            ViewBag.WebsiteID = new SelectList(db.GetWebsite(), "ID", "Name", news.WebsiteID);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.SelectByID(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var news = db.SelectByID(id);

            db.Delete(id);
            db.Save();

            using (LogRepository log = new LogRepository())
            {
                var logModel = new Log();
                logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                logModel.PubDate = DateTime.Now;

                using (AccountRepository account = new AccountRepository())
                {
                    var user = account.SelectByUserName(User.Identity.Name);
                    logModel.AccountID = user.ID;
                    logModel.Content = String.Format("{0} đã XÓA tin tức {1}", user.Fullname, news.Title);
                }

                log.Insert(logModel);
                log.Save();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll()
        {
            db.DeleteAll();
            db.Save();

            using (LogRepository log = new LogRepository())
            {
                var logModel = new Log();
                logModel.ID = Guid.NewGuid().ToString().Substring(0, 10);
                logModel.PubDate = DateTime.Now;

                using (AccountRepository account = new AccountRepository())
                {
                    var user = account.SelectByUserName(User.Identity.Name);
                    logModel.AccountID = user.ID;
                    logModel.Content = String.Format("{0} đã thực hiện chức năng XÓA HẾT", user.Fullname);
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
