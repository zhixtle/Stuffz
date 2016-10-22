using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserProfileModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int? CountryID { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public byte[] ProfilePic { get; set; }
        public string AboutMe { get; set; }
        public string EmailAddress { get; set; }
        public string IsFriend { get; set; }
    }
}