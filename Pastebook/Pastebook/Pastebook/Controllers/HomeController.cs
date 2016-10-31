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
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewsFeedPosts()
        {
            Managers.PostManager postManager = new Managers.PostManager();
            List<Models.PostModel> newsFeed = postManager.GetNewsFeed(Session["user"].ToString());
            return PartialView("PostsList", newsFeed);
        }
    }
}