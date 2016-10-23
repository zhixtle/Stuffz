using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class HomeController : Controller
    {
        private static Managers.CountryManager countryManager = new Managers.CountryManager();
        private static List<Models.CountryModel> countriesList = countryManager.GetCountriesList();

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
            Managers.UserManager userManager = new Managers.UserManager();
            Models.UserModel model = userManager.GetUser(Session["user"].ToString());
            ViewData["Countries"] = countriesList;
            return View(model);
        }

        public ActionResult Search()
        {
            return View();
        }

        public JsonResult GetSearchResults(string searchQuery)
        {
            return Json(new { results = searchQuery }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchResults(string searchQuery)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            List<Models.UserProfileModel> model = userManager.GetUsersSearchResults(searchQuery);
            return PartialView("SearchResults", model);
        }

        public ActionResult EditDetails(Models.UserModel model)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool editSuccess = userManager.EditUser(model, Session["user"].ToString());
            ViewData["Countries"] = countriesList;
            return RedirectToAction("Settings", model);
        }

        public ActionResult EditEmailPassword(Models.UserModel model)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool editSuccess = userManager.EditEmailPassword(model, Session["user"].ToString());
            ViewData["Countries"] = countriesList;
            return RedirectToAction("Settings", model);
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