using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookDataAccess;
using PastebookEntities;

namespace PastebookBusinessLogic
{
    public class LikeBL
    {
        private static DataAccess<LIKE> likeDataAccess = new DataAccess<LIKE>();

        public bool LikeStatus(LIKE like)
        {
            bool likeSuccess = likeDataAccess.Add(like);
            return likeSuccess;
        }

        public bool UnlikeStatus(LIKE like)
        {
            bool unlikeSuccess = likeDataAccess.Delete(like);
            return unlikeSuccess;
        }

        public LIKE GetLike(int postID, string username)
        {
            LIKE like = likeDataAccess.GetSingle(l => l.POST_ID == postID && l.USER.USER_NAME == username);
            return like;
        }

        public List<LIKE> GetLikesOnPost(int postID)
        {
            List<LIKE> likes = likeDataAccess.GetSelected(l => l.POST_ID == postID, "USER");
            return likes;
        }
    }
}
