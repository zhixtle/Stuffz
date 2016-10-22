using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class LikeModel
    {
        public int LikeID { get; set; }
        public int PostID { get; set; }
        public int LikedByID { get; set; }
        public string LikedByUsername { get; set; }
        public string LikedByName { get; set; }
    }
}