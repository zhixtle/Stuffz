using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class PostModel
    {
        public int PostID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public string ProfileOwnerName { get; set; }
        public string ProfileOwnerUsername { get; set; }
        public int ProfileOwnerID { get; set; }
        public string PosterName { get; set; }
        public string PosterUsername { get; set; }
        public int PosterID { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
    }
}