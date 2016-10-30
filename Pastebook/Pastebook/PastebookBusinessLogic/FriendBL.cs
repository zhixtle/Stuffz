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

        public bool DeleteFriend(FRIEND friend)
        {
            bool deleteSuccess = friendDataAccess.Delete(friend);
            return deleteSuccess;
        }

        public FRIEND GetFriendEntry(string username, int profileID)
        {
            List<FRIEND> usersFriends = GetFriendsAndRequests(username);
            FRIEND friend = usersFriends.Where(f => f.USER_ID == profileID || f.FRIEND_ID == profileID).SingleOrDefault();
            return friend;
        }

        public List<FRIEND> GetFriendsAndRequests(string username)
        {
            List<FRIEND> friends = friendDataAccess.GetSelected(f => f.USER.USER_NAME == username || f.USER1.USER_NAME == username)
                                                   .ToList();
            return friends;
        }

        public List<FRIEND> GetFriends(string username)
        {
            List<FRIEND> friends = friendDataAccess.GetSelected(f => f.USER.USER_NAME == username || f.USER1.USER_NAME == username, "USER", "USER1")
                                                         .Where(friend => friend.REQUEST == "N")
                                                         .ToList();

            List<FRIEND> friendsSwapped = SwapFriends(friends, username);
            return friendsSwapped;
        }

        public List<int> GetFriendIDs(string username)
        {
            List<FRIEND> friends = GetFriends(username);
            List<int> friendIDs = new List<int>();
            foreach (var item in friends)
            {
                friendIDs.Add(item.USER_ID);
            }
            return friendIDs;
        }

        public List<USER> GetFriendsList(string username)
        {
            List<FRIEND> friends = GetFriends(username);
            List<USER> friendsList = new List<USER>();
            foreach (var item in friends)
            {
                friendsList.Add(item.USER1);
            }
            return friendsList;
        }

        public List<FRIEND> SwapFriends(List<FRIEND> friends, string username)
        {
            List<FRIEND> friendsSwapped = new List<FRIEND>();
            foreach (var item in friends)
            {
                if (item.USER.USER_NAME == username)
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
                        CREATED_DATE = item.CREATED_DATE,
                        USER = item.USER1,
                        USER1 = item.USER
                    });
                }
            }
            return friendsSwapped;
        }
    }
}
