using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookDataAccess;
using PastebookEntities;

namespace PastebookBusinessLogic
{
    public class NotificationBL
    {
        private static DataAccess<NOTIFICATION> notificationDataAccess = new DataAccess<NOTIFICATION>();
        private static UserBL userBL = new UserBL();

        public bool AddNotification(NOTIFICATION notification)
        {
            bool addSuccess = notificationDataAccess.Add(notification);
            return addSuccess;
        }

        public List<NOTIFICATION> GetNotifications(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<NOTIFICATION> notifications = notificationDataAccess.GetSelected(n => n.RECEIVER_ID == userID && n.SEEN == "N")
                                                                     .OrderByDescending(n => n.CREATED_DATE)
                                                                     .ToList();
            return notifications;
        }

        public NOTIFICATION GetNotification(int notifID)
        {
            NOTIFICATION notification = notificationDataAccess.GetSingle(n => n.ID == notifID);
            return notification;
        }

        public List<NOTIFICATION> GetAllNotifiations(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<NOTIFICATION> notifications = notificationDataAccess.GetSelected(n => n.RECEIVER_ID == userID)
                                                                     .OrderByDescending(n => n.CREATED_DATE)
                                                                     .ToList();
            return notifications;
        }

        public bool SeeNotifications(List<NOTIFICATION> notifications)
        {
            foreach (var item in notifications)
            {
                item.SEEN = "Y";
            }
            bool seen = notificationDataAccess.EditMany(notifications);
            return seen;
        }

        public bool SeeNotification(NOTIFICATION notification)
        {
            notification.SEEN = "Y";
            bool seen = notificationDataAccess.Edit(notification);
            return seen;
        }
    }
}
