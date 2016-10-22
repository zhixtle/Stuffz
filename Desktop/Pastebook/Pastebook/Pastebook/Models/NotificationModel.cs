﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class NotificationModel
    {
        public int NotificationID { get; set; }
        public DateTime DateCreated { get; set; }
        public string NotifType { get; set; }
        public int SenderID { get; set; }
        public string SenderName { get; set; }
        public string SenderUsername { get; set; }
        public int ReceiverID { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverUsername { get; set; } 
        public int? PostID { get; set; }
    }
}