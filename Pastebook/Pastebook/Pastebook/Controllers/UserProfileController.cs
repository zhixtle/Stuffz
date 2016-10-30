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

        public JsonResult SaveAboutMe(string username, string aboutMeContent)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool saveSuccess = userManager.EditAboutMe(username, aboutMeContent);
            return Json(new { Status = saveSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveProfilePicture(HttpPostedFileBase file)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            Managers.ImageManager imageManager = new Managers.ImageManager();
            byte[] picByteArray = null;
            bool saveSuccess = false;
            if (file != null && imageManager.IsImage(file))
            {
                picByteArray = imageManager.imageToByteArray(file);
                saveSuccess = userManager.EditProfilePicture(Session["user"].ToString(), picByteArray);
            }
            TempData["savePicture"] = saveSuccess;
            return RedirectToAction("ViewProfile", new { profileUsername = Session["user"].ToString() });
        }

        public JsonResult SendFriendRequest(string profileUsername)
        { 
            Managers.FriendManager friendManager = new Managers.FriendManager();
            bool requestSent = friendManager.SendFriendRequest(Session["user"].ToString(), profileUsername);
            return Json(new { Status = requestSent }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmFriendRequest(string profileUsername)
        {
            Managers.FriendManager friendManager = new Managers.FriendManager();
            bool requestConfirmed = friendManager.ConfirmFriendRequest(Session["user"].ToString(), profileUsername);
            return Json(new { Status = requestConfirmed }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFriendRequest(string profileUsername)
        {
            Managers.FriendManager friendManager = new Managers.FriendManager();
            bool requestDeleted = friendManager.DeleteFriendRequest(Session["user"].ToString(), profileUsername);
            return Json(new { Status = requestDeleted }, JsonRequestBehavior.AllowGet);
        }
    }
}