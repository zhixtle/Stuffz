using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class SearchController : Controller
    {
        [Route("search")]
        [CustomAuthorize]
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
    }
}