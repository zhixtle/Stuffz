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

        public bool SendFriendRequest(string username, int profileID)
        {
            if (String.IsNullOrWhiteSpace(username) || profileID == 0)
            {
                return false;
            }

            FRIEND friendToAdd = new FRIEND()
            {
                USER_ID = userBL.GetIDByUsername(username),
                FRIEND_ID = profileID,
                CREATED_DATE = DateTime.Now,
                REQUEST = "Y",
                BLOCKED = "N"
            };
            bool requestSent = friendBL.AddFriend(friendToAdd);

            if(requestSent == false)
            {
                return false;
            }

            bool notifSent = notifManager.FriendRequestNotification(username, profileID);
            return (requestSent && notifSent);
        }

        public bool ConfirmFriendRequest(string username, int profileID)
        {
            if (String.IsNullOrWhiteSpace(username) || profileID == 0)
            {
                return false;
            }

            FRIEND friendToAdd = friendBL.GetFriendEntry(username, profileID);
            friendToAdd.REQUEST = "N";
            bool requestSent = friendBL.EditFriend(friendToAdd);
            return requestSent;
        }

        public bool DeleteFriendRequest(string username, int profileID)
        {
            if (String.IsNullOrWhiteSpace(username) || profileID == 0)
            {
                return false;
            }

            FRIEND friendToDelete = friendBL.GetFriendEntry(username, profileID);
            bool deleteSuccess = friendBL.DeleteFriend(friendToDelete);
            return deleteSuccess;
        }

        public List<Models.FriendModel> GetFriendsList(string username)
        {
            List<USER> friendsResults = friendBL.GetFriendsList(username);
            List<Models.FriendModel> friendsList = new List<Models.FriendModel>();
            foreach (var item in friendsResults)
            {
                friendsList.Add(new Models.FriendModel()
                {
                    FirstName = item.FIRST_NAME,
                    LastName = item.LAST_NAME,
                    Username = item.USER_NAME,
                    ProfilePicture = item.PROFILE_PIC
                });
            }
            return friendsList;
        }
    }
}