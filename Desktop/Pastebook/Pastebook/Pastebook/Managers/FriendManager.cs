using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;

namespace Pastebook.Managers
{
    public class FriendManager
    {
        private static FriendBL friendBL = new FriendBL();
        private static UserBL userBL = new UserBL();
        private static NotificationManager notifManager = new NotificationManager();

        public bool SendFriendRequest(string username, string profileUsername)
        {
            FRIEND friendToAdd = new FRIEND()
            {
                USER_ID = userBL.GetIDByUsername(username),
                FRIEND_ID = userBL.GetIDByUsername(profileUsername),
                CREATED_DATE = DateTime.Now,
                REQUEST = "Y",
                BLOCKED = "N"
            };
            bool requestSent = friendBL.AddFriend(friendToAdd);
            bool notifSent = notifManager.FriendRequestNotification(username, profileUsername);
            return (requestSent && notifSent);
        }

        public bool ConfirmFriendRequest(string username, string profileUsername)
        {
            FRIEND friendToAdd = new FRIEND()
            {
                USER_ID = userBL.GetIDByUsername(username),
                FRIEND_ID = userBL.GetIDByUsername(profileUsername),
                CREATED_DATE = DateTime.Now,
                REQUEST = "N",
                BLOCKED = "N"
            };
            bool requestSent = friendBL.EditFriend(friendToAdd);
            return requestSent;
        }

        public  List<Models.FriendModel> GetFriendsList(string username)
        {
            List<USER> friendsResults = friendBL.GetFriendsList(username);
            List<Models.FriendModel> friendsList = new List<Models.FriendModel>();
            foreach (var item in friendsResults)
            {
                friendsList.Add(new Models.FriendModel()
                {
                    UserID = item.ID,
                    FirstName = item.FIRST_NAME,
                    LastName = item.LAST_NAME,
                    Username = item.USER_NAME
                });
            }
            return friendsList;
        }
    }
}