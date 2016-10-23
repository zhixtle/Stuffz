using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class UserProfileController : Controller
    {
        public ActionResult TimelinePosts(string username)
        {
            Managers.PostManager postManager = new Managers.PostManager();
            List<Models.PostModel> timeline = postManager.GetTimeline(username);
            foreach (var item in timeline)
            {
                item.IsLiked = postManager.IsPostLikedByUser(item.PostID, Session["user"].ToString());
            }
            return PartialView("PostsList", timeline);
        }

        public ActionResult ViewProfile(string profileUsername)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            Models.UserProfileModel model = userManager.GetUserProfile(Session["user"].ToString(), profileUsername);
            return View(model);
        }

        public JsonResult SaveAboutMe(string content)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            return Json(new { Status = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendFriendRequest(string profileUsername)
        {
            bool requestSent = false;
            Managers.FriendManager friendManager = new Managers.FriendManager();
            requestSent = friendManager.SendFriendRequest(Session["user"].ToString(), profileUsername);
            return Json(new { Status = requestSent }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmFriendRequest(string profileUsername)
        {
            bool requestConfirmed = false;
            Managers.FriendManager friendManager = new Managers.FriendManager();
            requestConfirmed = friendManager.ConfirmFriendRequest(Session["user"].ToString(), profileUsername);
            return Json(new { Status = requestConfirmed }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFriendRequest(string profileUsername)
        {
            bool requestDeleted = false;
            Managers.FriendManager friendManager = new Managers.FriendManager();
            requestDeleted = friendManager.DeleteFriendRequest(Session["user"].ToString(), profileUsername);
            return Json(new { Status = requestDeleted }, JsonRequestBehavior.AllowGet);
        }
    }
}