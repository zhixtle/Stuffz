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
        [CustomAuthorize]
        public ActionResult Friends()
        {
            Managers.FriendManager friendManager = new Managers.FriendManager();
            List<Models.FriendModel> friendsList = friendManager.GetFriendsList(Session["user"].ToString());
            return View(friendsList);
        }
    }
}