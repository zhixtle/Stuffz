using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class NotificationController : Controller
    {
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

        [Route("notifications")]
        [CustomAuthorize]
        public ActionResult AllNotifications()
        {
            Managers.NotificationManager notifManager = new Managers.NotificationManager();
            List<Models.NotificationModel> notifsList = notifManager.GetAllNotifications(Session["user"].ToString());
            if (notifsList == null)
            {
                return null;
            }
            return View("AllNotifications", notifsList);
        }

        public ActionResult NotificationsButton()
        {
            Managers.NotificationManager notifManager = new Managers.NotificationManager();
            List<Models.NotificationModel> notifsList = notifManager.GetUnreadNotifications(Session["user"].ToString());
            if (notifsList == null)
            {
                return null;
            }
            return PartialView("NotificationsButton", notifsList);
        }

        public JsonResult SeeNotification(int notifID)
        {
            Managers.NotificationManager notifManager = new Managers.NotificationManager();
            bool seenNotifs = notifManager.SeeNotification(notifID);
            return Json(new { Status = seenNotifs }, JsonRequestBehavior.AllowGet);
        }
    }
}