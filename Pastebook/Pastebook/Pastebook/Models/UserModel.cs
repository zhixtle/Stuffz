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
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, ErrorMessage = "Password can only have up to 50 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [StringLength(50, ErrorMessage = "Confirm Password can only have up to 50 characters.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name can only have up to 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name can only have up to 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public int? CountryID { get; set; }

        [StringLength(50, ErrorMessage = "Mobile Number can only have up to 50 characters.")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "'Email Address is required.'")]
        [StringLength(50, ErrorMessage = "Email Address can only have up to 50 characters.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Email Address must be a valid email.")]
        public string EmailAddress { get; set; }
    }
}