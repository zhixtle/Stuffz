using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookDataAccess;
using PastebookEntities;

namespace PastebookBusinessLogic
{
    public class FriendBL
    {
        private static DataAccess<FRIEND> friendDataAccess = new DataAccess<FRIEND>();
        private static DataAccess<USER> userDataAccess = new DataAccess<USER>();
        private static UserBL userBL = new UserBL();

        public bool AddFriend(FRIEND friend)
        {
            bool addSuccess = friendDataAccess.Add(friend);
            return addSuccess;
        }

        public bool EditFriend(FRIEND friend)
        {
            bool editSuccess = friendDataAccess.Edit(friend);
            return editSuccess;
        }

        public List<FRIEND> GetFriendsAndRequests(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<FRIEND> friends = friendDataAccess.GetSelected(f => f.USER_ID == userID || f.FRIEND_ID == userID)
                                                   .ToList();

            List<FRIEND> friendsSwapped = SwapFriends(friends, userID);
            return friendsSwapped;
        }

        public List<FRIEND> GetFriends(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<FRIEND> friends = friendDataAccess.GetSelected(f => f.USER_ID == userID || f.FRIEND_ID == userID)
                                                         .Where(friend => friend.REQUEST == "N")
                                                         .ToList();

            List<FRIEND> friendsSwapped = SwapFriends(friends, userID);
            return friendsSwapped;
        }

        public List<USER> GetFriendsList(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<FRIEND> friends = GetFriends(username);

            List<USER> friendsList = new List<USER>();

            foreach (var item in friends)
            {
                USER friend = userDataAccess.GetSingle(u => u.ID == item.FRIEND_ID);
                friendsList.Add(friend);
            }
            return friendsList;
        }

        public string IsUserAFriend(string username, string profileUsername)
        {
            int userID = userBL.GetIDByUsername(username);
            int profileID = userBL.GetIDByUsername(profileUsername);
            List<FRIEND> friendResults = GetFriendsAndRequests(username);
            if (username == profileUsername)
            {
                return "Y";
            }
            else if (friendResults.Any(f => f.FRIEND_ID == profileID && f.REQUEST == "N"))
            {
                return "Y";
            }
            else if (friendResults.Any(f => f.FRIEND_ID == profileID && f.REQUEST == "Y"))
            {
                return "R";
            }
            else
            {
                return "N";
            }
        }

        public List<FRIEND> SwapFriends(List<FRIEND> friends, int userID)
        {
            List<FRIEND> friendsSwapped = new List<FRIEND>();
            foreach (var item in friends)
            {
                if (item.USER_ID == userID)
                {
                    friendsSwapped.Add(item);
                }
                else
                {
                    friendsSwapped.Add(new FRIEND()
                    {
                        ID = item.ID,
                        USER_ID = item.FRIEND_ID,
                        FRIEND_ID = item.USER_ID,
                        REQUEST = item.REQUEST,
                        BLOCKED = item.BLOCKED,
                        CREATED_DATE = item.CREATED_DATE
                    });
                }
            }
            return friendsSwapped;
        }
    }
}
