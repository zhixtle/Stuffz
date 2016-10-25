using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Posts(int id)
        {
            Managers.PostManager postManager = new Managers.PostManager();
            Models.PostModel model = postManager.GetPost(id);
            model.IsLiked = postManager.IsPostLikedByUser(id, Session["user"].ToString());
            return View(model);
        }

        public JsonResult AddPost(string content, string user)
        {
            string parsedContent = HttpUtility.HtmlDecode(content);
            bool postSuccess = false;
            string userToPostTo = (user == null) ? Session["user"].ToString() : user;
            if (String.IsNullOrWhiteSpace(parsedContent) == false)
            {
                Managers.PostManager postManager = new Managers.PostManager();
                Models.PostModel model = new Models.PostModel()
                {
                    Content = parsedContent,
                    PosterUsername = Session["user"].ToString(),
                    ProfileOwnerUsername = userToPostTo
                };
                postSuccess = postManager.PostStatus(model);
            }
            return Json(new { Status = postSuccess }, JsonRequestBehavior.AllowGet);
        }

        #region Likes

        public ActionResult Likes(string postID)
        {
            int id = Int32.Parse(postID);
            Managers.LikesManager likesManager = new Managers.LikesManager();
            List<Models.LikeModel> postLikes = likesManager.GetLikesOnPost(id);
            return PartialView("Likes", postLikes);
        }

        public JsonResult AddLike(string postID)
        {
            int id = Int32.Parse(postID);
            bool likeSuccess = false;
            if (id != 0)
            {
                Managers.LikesManager likesManager = new Managers.LikesManager();
                likeSuccess = likesManager.LikeStatus(Session["user"].ToString(), id);
            }
            return Json(new { Status = likeSuccess }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveLike(string postID)
        {
            int id = Int32.Parse(postID);
            bool unlikeSuccess = false;
            if (id != 0)
            {
                Managers.LikesManager likesManager = new Managers.LikesManager();
                unlikeSuccess = likesManager.UnlikeStatus(Session["user"].ToString(), id);
            }
            return Json(new { Status = unlikeSuccess }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Comments

        public ActionResult PostComments(string postID)
        {
            int id = Int32.Parse(postID);
            Managers.CommentManager commentsManager = new Managers.CommentManager();
            List<Models.CommentModel> postComments = commentsManager.GetCommentsOnPost(id);
            return PartialView("Comments", postComments);
        }

        public JsonResult AddComment(string postID, string content)
        {
            string parsedContent = HttpUtility.HtmlDecode(content);
            int id = Int32.Parse(postID);
            bool commentSuccess = false;
            if (id != 0 && String.IsNullOrWhiteSpace(parsedContent) == false)
            {
                Managers.CommentManager commentsManager = new Managers.CommentManager();
                Models.CommentModel model = new Models.CommentModel()
                {
                    PostID = id,
                    Content = parsedContent,
                    PosterUsername = Session["user"].ToString()
                };
                commentSuccess = commentsManager.CommentOnStatus(model);
            }
            return Json(new { Status = commentSuccess }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}