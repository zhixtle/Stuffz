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

        [Route("friends")]
        public ActionResult Friends()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Managers.FriendManager friendManager = new Managers.FriendManager();
            List<Models.FriendModel> friendsList = friendManager.GetFriendsList(Session["user"].ToString());
            return View(friendsList);
        }
    }
}