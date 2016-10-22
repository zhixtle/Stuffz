﻿using System;
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

        public bool LikeStatus(Models.LikeModel like)
        {
            LIKE newLike = new LIKE()
            {
                POST_ID = like.PostID,
                LIKED_BY = userBL.GetIDByUsername(like.LikedByUsername)
            };
            bool likeSuccess = likeBL.LikeStatus(newLike);
            int postOwnerID = postBL.GetUserByPostID(like.PostID);
            bool notifSent = notifManager.LikeNotification(newLike.LIKED_BY, postOwnerID, newLike.POST_ID);
            return (likeSuccess && notifSent);
        }

        public List<Models.LikeModel> GetLikesOnPost(int postID)
        {
            List<LIKE> likesResults = likeBL.GetLikesOnPost(postID);
            List<Models.LikeModel> likesOnPost = new List<Models.LikeModel>();
            foreach(var item in likesResults)
            {
                likesOnPost.Add(new Models.LikeModel()
                {
                    LikeID = item.ID,
                    PostID = item.POST_ID,
                    LikedByID = item.LIKED_BY,
                    LikedByName = userBL.GetUserByID(item.LIKED_BY)
                });
            }
            return likesOnPost;
        }
    }
}