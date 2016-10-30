using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;

namespace Pastebook.Managers
{
    public class LikesManager
    {
        private static LikeBL likeBL = new LikeBL();
        private static UserBL userBL = new UserBL();
        private static PostBL postBL = new PostBL();
        private static NotificationManager notifManager = new NotificationManager();

        public bool LikeStatus(string username, string postID)
        {
            int id = Int32.Parse(postID);
            LIKE newLike = new LIKE()
            {
                POST_ID = id,
                LIKED_BY = userBL.GetIDByUsername(username)
            };
            bool likeSuccess = likeBL.LikeStatus(newLike);
            int postOwnerID = postBL.GetUserByPostID(id);
            bool notifSent = notifManager.LikeNotification(newLike.LIKED_BY, postOwnerID, newLike.POST_ID);
            return (likeSuccess && notifSent);
        }

        public bool UnlikeStatus(string username, string postID)
        {
            int id = Int32.Parse(postID);
            LIKE like = likeBL.GetLike(id, username);
            bool unlikeSuccess = likeBL.UnlikeStatus(like);
            return unlikeSuccess;
        }

        public List<Models.LikeModel> GetLikesOnPost(string postID)
        {
            int id = Int32.Parse(postID);
            List<LIKE> likesResults = likeBL.GetLikesOnPost(id);
            List<Models.LikeModel> likesOnPost = new List<Models.LikeModel>();
            foreach(var item in likesResults)
            {
                likesOnPost.Add(new Models.LikeModel()
                {
                    LikedByName = item.USER.FIRST_NAME + " " + item.USER.LAST_NAME
                });
            }
            return likesOnPost;
        }
    }
}