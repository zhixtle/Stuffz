using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookDataAccess;
using PastebookEntities;

namespace PastebookBusinessLogic
{
    public class CommentBL
    {
        private static DataAccess<COMMENT> commentDataAccess = new DataAccess<COMMENT>();

        public int CommentOnStatus(COMMENT comment)
        {
            int commentID = commentDataAccess.AddWithID(comment).ID;
            return commentID;
        }

        public List<COMMENT> GetCommentsOnPost(int postID)
        {
            List<COMMENT> comments = commentDataAccess.GetSelected(c => c.POST_ID == postID, "USER");
            return comments;
        }
    }
}
