using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;

namespace Pastebook.Managers
{
    public class CommentManager
    {
        private static CommentBL commentBL = new CommentBL();
        private static UserBL userBL = new UserBL();
        private static PostBL postBL = new PostBL();
        private static NotificationManager notifManager = new NotificationManager();

        public bool CommentOnStatus(Models.CommentModel comment)
        {
            COMMENT newComment = new COMMENT()
            {
                CONTENT = comment.Content,
                DATE_CREATED = DateTime.Now,
                POSTER_ID = userBL.GetIDByUsername(comment.PosterUsername),
                POST_ID = comment.PostID
            };
            int commentSuccess = commentBL.CommentOnStatus(newComment);
            int profileOwnerID = postBL.GetUserByPostID(comment.PostID);
            bool notifSent = notifManager.CommentNotification(newComment.POSTER_ID, profileOwnerID, comment.PostID, commentSuccess);
            return ((commentSuccess != 0) && notifSent);
        }

        public List<Models.CommentModel> GetCommentsOnPost(int postID)
        {
            List<COMMENT> commentsResults = commentBL.GetCommentsOnPost(postID);
            List<Models.CommentModel> commentsOnPost = new List<Models.CommentModel>();
            foreach (var item in commentsResults)
            {
                commentsOnPost.Add(new Models.CommentModel()
                {
                    CommentID = item.ID,
                    Content = item.CONTENT,
                    DateCreated = item.DATE_CREATED,
                    PosterID = item.POSTER_ID,
                    PosterName = userBL.GetUserByID(item.POSTER_ID),
                    PosterUsername  = userBL.GetUsernameByID(item.POSTER_ID),
                    PostID = item.POST_ID
                });
            }
            return commentsOnPost;
        }
    }
}