using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            if (Session["user" ] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult NewsFeedPosts()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Managers.PostManager postManager = new Managers.PostManager();
            List<Models.PostModel> newsFeed = postManager.GetNewsFeed(Session["user"].ToString());
            return PartialView("PostsList", newsFeed);
        }
    }
}