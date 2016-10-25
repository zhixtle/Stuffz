using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class PostModel
    {
        public int PostID { get; set; }

        [DisplayFormat(DataFormatString = "{0: MMM dd yyyy h:mmtt}")]
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public string ProfileOwnerName { get; set; }
        public string ProfileOwnerUsername { get; set; }
        public int ProfileOwnerID { get; set; }
        public string PosterName { get; set; }
        public string PosterUsername { get; set; }
        public byte[] PosterProfilePicture { get; set; }
        public int PosterID { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
    }
}