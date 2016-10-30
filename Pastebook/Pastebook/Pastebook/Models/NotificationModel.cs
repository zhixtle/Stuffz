using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class NotificationModel
    {
        [DisplayFormat(DataFormatString = "{0: MMM dd yyyy h:mmtt}")]
        public DateTime DateCreated { get; set; }

        public int NotificationID { get; set; }
        public string NotifType { get; set; }
        public string SenderName { get; set; }
        public string SenderUsername { get; set; }
        public int? PostID { get; set; }
    }
}