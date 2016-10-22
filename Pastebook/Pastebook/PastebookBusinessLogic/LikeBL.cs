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

        public List<LIKE> GetLikesOnPost(int postID)
        {
            List<LIKE> likes = likeDataAccess.GetSelected(l => l.POST_ID == postID);
            return likes;
        }
    }
}
