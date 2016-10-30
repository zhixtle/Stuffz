﻿using System;
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
                PosterName = userBL.GetUserByID(postResult.POSTER_ID),
                PosterUsername = userBL.GetUsernameByID(postResult.POSTER_ID),
                ProfileOwnerID = postResult.PROFILE_OWNER_ID,
                ProfileOwnerName = userBL.GetUserByID(postResult.PROFILE_OWNER_ID),
                ProfileOwnerUsername = userBL.GetUsernameByID(postResult.PROFILE_OWNER_ID),
                LikesCount = postBL.GetLikesCountOnPost(postResult.ID),
                IsLiked = IsPostLikedByUser(postResult.ID, currentUser)
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
                    PosterName = userBL.GetUserByID(item.POSTER_ID),
                    PosterUsername = userBL.GetUsernameByID(item.POSTER_ID),
                    ProfileOwnerID = item.PROFILE_OWNER_ID,
                    ProfileOwnerUsername = userBL.GetUsernameByID(item.PROFILE_OWNER_ID),
                    ProfileOwnerName = userBL.GetUserByID(item.PROFILE_OWNER_ID),
                    LikesCount = postBL.GetLikesCountOnPost(item.ID),
                    IsLiked = IsPostLikedByUser(item.ID, currentUser)
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
                    PosterName = userBL.GetUserByID(item.POSTER_ID),
                    PosterUsername = userBL.GetUsernameByID(item.POSTER_ID),
                    ProfileOwnerID = item.PROFILE_OWNER_ID,
                    ProfileOwnerName = userBL.GetUserByID(item.PROFILE_OWNER_ID),
                    ProfileOwnerUsername = userBL.GetUsernameByID(item.PROFILE_OWNER_ID),
                    LikesCount = postBL.GetLikesCountOnPost(item.ID),
                    IsLiked = IsPostLikedByUser(item.ID, username)
                });
            }
            return newsFeed;
        }

        public bool IsPostLikedByUser(int postID, string username)
        {
            bool isLiked = postBL.IsPostLikedByUser(postID, username);
            return isLiked;
        }
    }
}