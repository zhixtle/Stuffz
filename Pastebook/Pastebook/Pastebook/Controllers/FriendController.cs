using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class FriendController : Controller
    {
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