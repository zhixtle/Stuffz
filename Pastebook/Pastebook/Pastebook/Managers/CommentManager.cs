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

        public bool CommentOnStatus(string postID, string content, string user)
        {
            string parsedContent = HttpUtility.HtmlDecode(content);
            int id = Int32.Parse(postID);
            if (parsedContent.Length > 1000 || id == 0 || String.IsNullOrWhiteSpace(user))
            {
                return false;
            }

            COMMENT newComment = new COMMENT()
            {
                CONTENT = parsedContent,
                DATE_CREATED = DateTime.Now,
                POSTER_ID = userBL.GetIDByUsername(user),
                POST_ID = id
            };
            int commentSuccess = commentBL.CommentOnStatus(newComment);

            if (commentSuccess == 0)
            {
                return false;
            }

            int profileOwnerID = postBL.GetUserByPostID(id);
            bool notifSent = notifManager.CommentNotification(newComment.POSTER_ID, profileOwnerID, id, commentSuccess);
            return ((commentSuccess != 0) && notifSent);
        }

        public List<Models.CommentModel> GetCommentsOnPost(string postID)
        {
            int id = Int32.Parse(postID);
            List<COMMENT> commentsResults = commentBL.GetCommentsOnPost(id);
            List<Models.CommentModel> commentsOnPost = new List<Models.CommentModel>();
            foreach (var item in commentsResults)
            {
                commentsOnPost.Add(new Models.CommentModel()
                {
                    Content = item.CONTENT,
                    DateCreated = item.DATE_CREATED,
                    PosterName = item.USER.FIRST_NAME + " " + item.USER.LAST_NAME,
                    PosterUsername = item.USER.USER_NAME,
                    PostID = item.POST_ID
                });
            }
            return commentsOnPost;
        }
    }
}