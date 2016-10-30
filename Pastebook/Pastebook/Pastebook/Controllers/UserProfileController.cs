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
            List<Models.PostModel> timeline = postManager.GetTimeline(username, Session["user"].ToString());
            return PartialView("PostsList", timeline);
        }

        [Route("user/{profileUsername}")]
        public ActionResult ViewProfile(string profileUsername)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Managers.UserManager userManager = new Managers.UserManager();
            Models.UserProfileModel model = userManager.GetUserProfile(Session["user"].ToString(), profileUsername);
            ViewData["savePicture"] = TempData["savePicture"];
            return View(model);
        }

        public JsonResult SaveAboutMe(string aboutMeContent)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool saveSuccess = userManager.EditAboutMe(Session["user"].ToString(), aboutMeContent);
            return Json(new { Status = saveSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveProfilePicture(HttpPostedFileBase file)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool saveSuccess = userManager.EditProfilePicture(Session["user"].ToString(), file);
            TempData["savePicture"] = saveSuccess;
            return RedirectToAction("ViewProfile", new { profileUsername = Session["user"].ToString() });
        }

        public JsonResult SendFriendRequest(int profileID)
        { 
            Managers.FriendManager friendManager = new Managers.FriendManager();
            bool requestSent = friendManager.SendFriendRequest(Session["user"].ToString(), profileID);
            return Json(new { Status = requestSent }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmFriendRequest(int profileID)
        {
            Managers.FriendManager friendManager = new Managers.FriendManager();
            bool requestConfirmed = friendManager.ConfirmFriendRequest(Session["user"].ToString(), profileID);
            return Json(new { Status = requestConfirmed }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFriendRequest(int profileID)
        {
            Managers.FriendManager friendManager = new Managers.FriendManager();
            bool requestDeleted = friendManager.DeleteFriendRequest(Session["user"].ToString(), profileID);
            return Json(new { Status = requestDeleted }, JsonRequestBehavior.AllowGet);
        }
    }
}