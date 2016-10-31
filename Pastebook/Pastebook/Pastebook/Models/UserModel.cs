using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username can only have up to 50 characters.")]
        [RegularExpression("^((([_.]?)[a-zA-Z0-9]+([_.])?[a-zA-Z0-9]+)*([_.]?))$", ErrorMessage = "Username can only contain letters, numbers, periods, and underscores. Two or more consecutive periods or underscores are not allowed. (e.g. user1, user_7, user.name)")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password must have at least 4 to 50 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Confirm Password must have at least 4 to 50 characters.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name can only have up to 50 characters.")]
        [RegularExpression("^(([a-zA-Z0-9]+[ -.']?[a-zA-Z0-9]+)*['.]?)$", ErrorMessage = "First Name can only contain letters, numbers, and no two or more consecutive spaces, dashes, periods, or apostrophes.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name can only have up to 50 characters.")]
        [RegularExpression("^(([a-zA-Z0-9]+[ -.']?[a-zA-Z0-9]+)*['.]?)$", ErrorMessage = "Last Name can only contain letters, numbers, and no two or more consecutive spaces, dashes, periods, or apostrophes.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date, ErrorMessage = "Birthday must be a valid date.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public int? CountryID { get; set; }

        [StringLength(50, ErrorMessage = "Mobile Number can only have up to 50 characters.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Mobile Number is invalid.")]
        [Phone(ErrorMessage = "Mobile Number is invalid.")]
        public string MobileNumber { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(50, ErrorMessage = "Email Address can only have up to 50 characters.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Email Address must be a valid email.")]
        public string EmailAddress { get; set; }
    }
}