using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class PostController : Controller
    {
        [Route("posts/{id:int}")]
        public ActionResult Posts(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Managers.PostManager postManager = new Managers.PostManager();
            Models.PostModel model = postManager.GetPost(id, Session["user"].ToString());
            return View(model);
        }

        public JsonResult AddPost(string content, string user)
        {
            bool postSuccess = false;
            string userToPostTo = (user == null) ? Session["user"].ToString() : user;
            if (String.IsNullOrWhiteSpace(content) == false)
            {
                Managers.PostManager postManager = new Managers.PostManager();
                postSuccess = postManager.PostStatus(content, userToPostTo, Session["user"].ToString());
            }
            return Json(new { Status = postSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Likes(string postID)
        {
            Managers.LikesManager likesManager = new Managers.LikesManager();
            List<Models.LikeModel> postLikes = likesManager.GetLikesOnPost(postID);
            return PartialView("Likes", postLikes);
        }

        public JsonResult AddLike(string postID)
        {
            bool likeSuccess = false;
            if (postID != null)
            {
                Managers.LikesManager likesManager = new Managers.LikesManager();
                likeSuccess = likesManager.LikeStatus(Session["user"].ToString(), postID);
            }
            return Json(new { Status = likeSuccess }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveLike(string postID)
        {
            bool unlikeSuccess = false;
            if (postID != null)
            {
                Managers.LikesManager likesManager = new Managers.LikesManager();
                unlikeSuccess = likesManager.UnlikeStatus(Session["user"].ToString(), postID);
            }
            return Json(new { Status = unlikeSuccess }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PostComments(string postID)
        {
            Managers.CommentManager commentsManager = new Managers.CommentManager();
            List<Models.CommentModel> postComments = commentsManager.GetCommentsOnPost(postID);
            return PartialView("Comments", postComments);
        }

        public JsonResult AddComment(string postID, string content)
        {
            bool commentSuccess = false;
            if (postID != null && String.IsNullOrWhiteSpace(content) == false)
            {
                Managers.CommentManager commentsManager = new Managers.CommentManager();
                commentSuccess = commentsManager.CommentOnStatus(postID, content, Session["user"].ToString());
            }
            return Json(new { Status = commentSuccess }, JsonRequestBehavior.AllowGet);
        }
    }
}