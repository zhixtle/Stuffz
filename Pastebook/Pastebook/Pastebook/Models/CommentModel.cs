using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class CommentModel
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int PosterID { get; set; }
        public string PosterName { get; set; }
        public string PosterUsername { get; set; }
        public string Content { get; set; }

        [DisplayFormat (DataFormatString = "{0: MMM dd yyyy h:mmtt}")]
        public DateTime DateCreated { get; set; }
    }
}