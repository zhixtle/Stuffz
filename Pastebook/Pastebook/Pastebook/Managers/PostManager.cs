using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;

namespace Pastebook.Managers
{
    public class PostManager
    {
        private static PostBL postBL = new PostBL();
        private static UserBL userBL = new UserBL();

        public bool PostStatus(string content, string user, string poster)
        {
            string parsedContent = HttpUtility.HtmlDecode(content);
            if (parsedContent.Length > 1000)
            {
                return false;
            }
            else
            {
                POST newPost = new POST()
                {
                    CONTENT = parsedContent,
                    CREATED_DATE = DateTime.Now,
                    POSTER_ID = userBL.GetIDByUsername(poster),
                    PROFILE_OWNER_ID = userBL.GetIDByUsername(user)
                };
                bool postSuccess = postBL.PostStatus(newPost);
                return postSuccess;
            }
        }

        public Models.PostModel GetPost(int postID, string currentUser)
        {
            POST postResult = postBL.GetPost(postID);
            Models.PostModel post = new Models.PostModel() {
                Content = postResult.CONTENT,
                DateCreated = postResult.CREATED_DATE,
                PostID = postResult.ID,
                PosterID = postResult.POSTER_ID,
                PosterName = postResult.USER.FIRST_NAME + " " + postResult.USER.LAST_NAME,
                PosterUsername = postResult.USER.USER_NAME,
                ProfileOwnerID = postResult.PROFILE_OWNER_ID,
                ProfileOwnerName = postResult.USER1.FIRST_NAME + " " + postResult.USER1.LAST_NAME,
                ProfileOwnerUsername = postResult.USER1.USER_NAME,
                LikesCount = postResult.LIKEs.Count,
                IsLiked = IsPostLikedByUser(currentUser, postResult.LIKEs.ToList())
            };
            return post;
        }

        public List<Models.PostModel> GetTimeline(string username, string currentUser)
        {
            List<POST> postsResults = postBL.GetTimeline(username);
            List<Models.PostModel> newsFeed = new List<Models.PostModel>();
            foreach (var item in postsResults)
            {
                newsFeed.Add(new Models.PostModel()
                {
                    Content = item.CONTENT,
                    DateCreated = item.CREATED_DATE,
                    PostID = item.ID,
                    PosterID = item.POSTER_ID,
                    PosterName =  item.USER.FIRST_NAME + " "  + item.USER.LAST_NAME,
                    PosterUsername = item.USER.USER_NAME,
                    ProfileOwnerID = item.PROFILE_OWNER_ID,
                    ProfileOwnerUsername = item.USER1.USER_NAME,
                    ProfileOwnerName = item.USER1.FIRST_NAME + " " + item.USER1.LAST_NAME,
                    LikesCount = item.LIKEs.Count,
                    IsLiked = IsPostLikedByUser(currentUser, item.LIKEs.ToList())
                });
            }
            return newsFeed;
        }

        public List<Models.PostModel> GetNewsFeed(string username)
        {
            List<POST> postsResults = postBL.GetNewsFeed(username);
            List<Models.PostModel> newsFeed = new List<Models.PostModel>();
            foreach (var item in postsResults)
            {
                newsFeed.Add(new Models.PostModel()
                {
                    Content = item.CONTENT,
                    DateCreated = item.CREATED_DATE,
                    PostID = item.ID,
                    PosterID = item.POSTER_ID,
                    PosterName = item.USER.FIRST_NAME + " " + item.USER.LAST_NAME,
                    PosterUsername = item.USER.USER_NAME,
                    ProfileOwnerID = item.PROFILE_OWNER_ID,
                    ProfileOwnerUsername = item.USER1.USER_NAME,
                    ProfileOwnerName = item.USER1.FIRST_NAME + " " + item.USER1.LAST_NAME,
                    LikesCount = item.LIKEs.Count,
                    IsLiked = IsPostLikedByUser(username, item.LIKEs.ToList())
                });
            }
            return newsFeed;
        }

        private bool IsPostLikedByUser(string username, List<LIKE> postLikes)
        {
            bool isLiked = postLikes.Any(p => p.USER.USER_NAME == username);
            return isLiked;
        }
    }
}