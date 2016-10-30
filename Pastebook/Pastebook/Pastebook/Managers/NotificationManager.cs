using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;

namespace Pastebook.Managers
{
    public class NotificationManager
    {
        private static NotificationBL notificationBL = new NotificationBL();
        private static UserBL userBL = new UserBL();

        public bool FriendRequestNotification(string username, int profileID)
        {
            NOTIFICATION newnotif = new NOTIFICATION()
            {
                NOTIF_TYPE = "F",
                SENDER_ID = userBL.GetIDByUsername(username),
                RECEIVER_ID = profileID,
                CREATED_DATE = DateTime.Now,
                SEEN = "N"
            };
            bool addNotif = notificationBL.AddNotification(newnotif);
            return addNotif;
        }

        public bool LikeNotification(int userID, int postOwnerID, int postID)
        {
            NOTIFICATION newnotif = new NOTIFICATION()
            {
                NOTIF_TYPE = "L",
                SENDER_ID = userID,
                RECEIVER_ID = postOwnerID,
                CREATED_DATE = DateTime.Now,
                SEEN = "N",
                POST_ID = postID
            };
            bool addNotif = notificationBL.AddNotification(newnotif);
            return addNotif;
        }

        public bool CommentNotification(int userID, int profileOwnerID, int postID, int commentID)
        {
            NOTIFICATION newnotif = new NOTIFICATION()
            {
                NOTIF_TYPE = "C",
                SENDER_ID = userID,
                RECEIVER_ID = profileOwnerID,
                CREATED_DATE = DateTime.Now,
                SEEN = "N",
                POST_ID = postID,
                COMMENT_ID = commentID
            };
            bool addNotif = notificationBL.AddNotification(newnotif);
            return addNotif;
        }

        public List<Models.NotificationModel> GetUnreadNotifications(string username)
        {
            List<NOTIFICATION> notificationsResults = notificationBL.GetNotifications(username);
            List<Models.NotificationModel> notifs = new List<Models.NotificationModel>();
            foreach (var item in notificationsResults)
            {
                notifs.Add(new Models.NotificationModel()
                {
                    NotificationID = item.ID,
                    DateCreated = item.CREATED_DATE,
                    NotifType = item.NOTIF_TYPE,
                    PostID = item.POST_ID,
                    SenderName = item.USER1.FIRST_NAME + " " + item.USER1.LAST_NAME,
                    SenderUsername = item.USER1.USER_NAME
                });
            }
            return notifs;
        }

        public List<Models.NotificationModel> GetAllNotifications(string username)
        {
            List<NOTIFICATION> notificationsResults = notificationBL.GetAllNotifiations(username);
            List<Models.NotificationModel> notifs = new List<Models.NotificationModel>();
            foreach (var item in notificationsResults)
            {
                notifs.Add(new Models.NotificationModel()
                {
                    NotificationID = item.ID,
                    DateCreated = item.CREATED_DATE,
                    NotifType = item.NOTIF_TYPE,
                    PostID = item.POST_ID,
                    SenderName = item.USER1.FIRST_NAME + " " + item.USER1.LAST_NAME,
                    SenderUsername = item.USER1.USER_NAME
                });
            }
            return notifs;
        }

        public bool SeeNotifications(string username)
        {
            List<NOTIFICATION> notificationsResults = notificationBL.GetNotifications(username);
            bool seeSuccess = notificationBL.SeeNotifications(notificationsResults);
            return seeSuccess;
        }

        public bool SeeNotification(int notifID)
        {
            NOTIFICATION notification = notificationBL.GetNotification(notifID);
            bool seeSuccess = notificationBL.SeeNotification(notification);
            return seeSuccess;
        }
    }
}