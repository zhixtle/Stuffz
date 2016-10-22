using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["user" ] == null)
            {
                return RedirectToAction("Login", "LoginRegister");
            }
            return View();
        }

        public ActionResult NewsFeedPosts()
        {
            Managers.PostManager postManager = new Managers.PostManager();
            List<Models.PostModel> newsFeed = postManager.GetNewsFeed(Session["user"].ToString());
            foreach (var item in newsFeed)
            {
                item.IsLiked = postManager.IsPostLikedByUser(item.PostID, Session["user"].ToString());
            }
            return PartialView("PostsList", newsFeed);
        }

        public ActionResult UserLogout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }


        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Friends()
        {
            Managers.FriendManager friendManager = new Managers.FriendManager();
            List<Models.FriendModel> friendsList = friendManager.GetFriendsList(Session["user"].ToString());
            return View(friendsList);
        }

        public ActionResult Notifications()
        {
            Managers.NotificationManager notifManager = new Managers.NotificationManager();
            List<Models.NotificationModel> notifsList = notifManager.GetUnreadNotifications(Session["user"].ToString());
            if (notifsList == null)
            {
                return null;
            }
            return PartialView("Notifications", notifsList);
        }

        public JsonResult SeeNotifications()
        {
            Managers.NotificationManager notifManager = new Managers.NotificationManager();
            bool seenNotifs = notifManager.SeeNotifications(Session["user"].ToString());
            return Json(new { Status = seenNotifs }, JsonRequestBehavior.AllowGet);
        }
    }
}