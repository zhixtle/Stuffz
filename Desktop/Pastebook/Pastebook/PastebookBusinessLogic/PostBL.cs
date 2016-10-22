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
            POST post = postDataAccess.GetSingle(p => p.ID == postID);
            return post;
        }

        public List<POST> GetTimeline(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<POST> posts = postDataAccess.GetSelected(p => p.PROFILE_OWNER_ID == userID || p.POSTER_ID == userID)
                                             .OrderByDescending(p => p.CREATED_DATE)
                                             .ToList();
            return posts;
        }

        public List<POST> GetNewsFeed(string username)
        {
            int userID = userBL.GetIDByUsername(username);
            List<FRIEND> userFriends = friendBL.GetFriends(username);
            List<POST> posts = postDataAccess.GetAll();
            List<POST> filteredPosts = new List<POST>();
            foreach (var item in posts)
            {
                if(item.PROFILE_OWNER_ID == userID ||
                   item.POSTER_ID == userID ||
                   userFriends.Any(f => f.FRIEND_ID == item.PROFILE_OWNER_ID && f.FRIEND_ID == item.POSTER_ID))
                {
                    filteredPosts.Add(item);
                }
            }
            return filteredPosts.OrderByDescending(p => p.CREATED_DATE).ToList();
        }

        public int GetLikesCountOnPost(int postID)
        {
            int likesCount = likeBL.GetLikesOnPost(postID).Count;
            return likesCount;
        }

        public bool IsPostLikedByUser(int postID, string username)
        {
            int id = userBL.GetIDByUsername(username);
            bool isLiked = likeBL.GetLikesOnPost(postID).Any(p => p.LIKED_BY == id);
            return isLiked;
        }

        public int GetUserByPostID(int postID)
        {
            int user = postDataAccess.GetSingle(p => p.ID == postID).PROFILE_OWNER_ID;
            return user;
        }
    }
}
