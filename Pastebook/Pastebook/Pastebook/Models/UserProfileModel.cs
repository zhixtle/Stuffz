using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserProfileModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birthday")]
        [DisplayFormat(DataFormatString = "{0: MMMM d, yyyy}")]
        public DateTime Birthday { get; set; }

        public int? CountryID { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }
        public byte[] ProfilePic { get; set; }

        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public string IsFriend { get; set; }
    }
}