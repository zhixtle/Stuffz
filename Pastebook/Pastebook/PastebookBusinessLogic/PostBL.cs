using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookDataAccess;
using PastebookEntities;

namespace PastebookBusinessLogic
{
    public class PostBL
    {
        private static DataAccess<POST> postDataAccess = new DataAccess<POST>();
        private static LikeBL likeBL = new LikeBL();
        private static UserBL userBL = new UserBL();
        private static FriendBL friendBL = new FriendBL();


        public bool PostStatus(POST post)
        {
            bool postSuccess = postDataAccess.Add(post);
            return postSuccess;
        }

        public POST GetPost(int postID)
        {
            POST post = postDataAccess.GetSingle(p => p.ID == postID, "USER", "USER1", "LIKEs", "LIKEs.USER");
            return post;
        }

        public List<POST> GetTimeline(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<POST> posts = postDataAccess.GetSelected(p => p.PROFILE_OWNER_ID == userID || p.POSTER_ID == userID, "USER", "USER1", "LIKEs", "LIKEs.USER")
                                             .OrderByDescending(p => p.CREATED_DATE)
                                             .Take(100)
                                             .ToList();
            return posts;
        }

        public List<POST> GetNewsFeed(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<int> userFriends = friendBL.GetFriendIDs(username);
            List<POST> friendsPosts = postDataAccess.GetSelected(p => userFriends.Any(f => f.Equals(p.PROFILE_OWNER_ID) && f.Equals(p.POSTER_ID)),
                                                                "USER", "USER1", "LIKEs", "LIKEs.USER");
            List<POST> userPosts = postDataAccess.GetSelected(p => p.PROFILE_OWNER_ID == userID || p.POSTER_ID == userID,
                                                             "USER", "USER1", "LIKEs", "LIKEs.USER");
            List<POST> posts = userPosts;
            posts.AddRange(friendsPosts);
            return posts.OrderByDescending(p => p.CREATED_DATE).ToList();
        }

        public int GetUserByPostID(int postID)
        {
            int user = postDataAccess.GetSingle(p => p.ID == postID).PROFILE_OWNER_ID;
            return user;
        }
    }
}
