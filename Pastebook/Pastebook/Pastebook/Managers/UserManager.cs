﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;
using System.Text.RegularExpressions;

namespace Pastebook.Managers
{
    public class UserManager
    {
        private static UserBL userBL = new UserBL();
        private static ImageManager imageManager = new ImageManager();

        public Models.UserProfileModel GetUserProfile(string username, string profileUsername)
        {
            USER userResult = userBL.GetUserProfile(profileUsername);
            List<FRIEND> userFriends = userResult.FRIENDs.ToList();
            userFriends.AddRange(userResult.FRIENDs1.ToList());
            Models.UserProfileModel user = new Models.UserProfileModel()
            {
                UserID = userResult.ID,
                AboutMe = userResult.ABOUT_ME,
                Birthday = userResult.BIRTHDAY,
                Country = userResult.REF_COUNTRY.COUNTRY,
                EmailAddress = userResult.EMAIL_ADDRESS,
                FirstName = userResult.FIRST_NAME,
                Gender = GenderDisplay(userResult.GENDER),
                LastName = userResult.LAST_NAME,
                MobileNumber = userResult.MOBILE_NO,
                ProfilePic = userResult.PROFILE_PIC,
                Username = userResult.USER_NAME,
                IsFriend = IsUserAFriend(username, userResult.ID, userFriends)
            };
            return user;
        }

        public string IsUserAFriend(string username, int profileID, List<FRIEND> friends)
        {
            int userID = userBL.GetIDByUsername(username);
            if (userID == profileID)
            {
                return "Y";
            }
            else if (friends.Any(f => (f.FRIEND_ID == userID || f.USER_ID == userID) && f.REQUEST == "N"))
            {
                return "Y";
            }
            else if (friends.Any(f => (f.FRIEND_ID == userID && f.USER_ID == profileID) && f.REQUEST == "Y"))
            {
                return "C";
            }
            else if (friends.Any(f => (f.USER_ID == userID && f.FRIEND_ID == profileID) && f.REQUEST == "Y"))
            {
                return "R";
            }
            else
            {
                return "N";
            }
        }

        public Models.UserModel GetUser(string username)
        {
            USER userResult = userBL.GetUser(username);
            Models.UserModel user = new Models.UserModel()
            {
                Username = userResult.USER_NAME,
                FirstName = userResult.FIRST_NAME,
                LastName = userResult.LAST_NAME,
                Birthday = userResult.BIRTHDAY,
                CountryID = userResult.COUNTRY_ID,
                MobileNumber = userResult.MOBILE_NO,
                Gender = GenderDisplay(userResult.GENDER),
                Password = userResult.PASSWORD,
                EmailAddress = userResult.EMAIL_ADDRESS
            };
            return user;
        }

        public bool EditUser(Models.UserModel model, string username)
        {
            if (model == null || String.IsNullOrWhiteSpace(username) ||
                String.IsNullOrWhiteSpace(model.Username) || model.Username.Length > 50 || !IsValidUsername(model.Username) ||
                String.IsNullOrWhiteSpace(model.FirstName) || model.FirstName.Length > 50 || !IsValidName(model.FirstName) ||
                String.IsNullOrWhiteSpace(model.LastName) || model.LastName.Length > 50 || !IsValidName(model.LastName) ||
                model.Birthday == null || model.Birthday.ToShortDateString() == "1/1/0001")
            {
                return false;
            }

            if (model.MobileNumber != null)
            {
                if (model.MobileNumber.Length > 50)
                {
                    return false;
                }
            }

            USER userResult = userBL.GetUser(username);
            userResult.USER_NAME = model.Username;
            userResult.FIRST_NAME = model.FirstName;
            userResult.LAST_NAME = model.LastName;
            userResult.BIRTHDAY = model.Birthday;
            userResult.COUNTRY_ID = model.CountryID;
            userResult.MOBILE_NO = model.MobileNumber;
            userResult.GENDER = model.Gender;
            bool editSuccess = userBL.EditUser(userResult);
            return editSuccess;
        }

        private bool IsValidUsername(string username)
        {
            Regex rgx = new Regex("^((([_.]?)[a-zA-Z0-9]+([_.])?[a-zA-Z0-9]+)*([_.]?))$");
            bool isValid = rgx.IsMatch(username);
            return isValid;
        }

        private bool IsValidName(string name)
        {
            Regex rgx = new Regex("^(([a-zA-Z0-9]+[ -.']?[a-zA-Z0-9]+)*['.]?)$");
            bool isValid = rgx.IsMatch(name);
            return isValid;
        }

        public bool EditEmail(string email, string username)
        {
            string parsedEmail = HttpUtility.HtmlDecode(email);
            if (String.IsNullOrWhiteSpace(parsedEmail) || String.IsNullOrWhiteSpace(username) || parsedEmail.Length > 50 || !IsValidEmail(parsedEmail))
            {
                return false;
            }

            USER userResult = userBL.GetUser(username);
            userResult.EMAIL_ADDRESS = parsedEmail;
            bool editSuccess = userBL.EditUserEmail(userResult);
            return editSuccess;
        }

        private bool IsValidEmail(string email)
        {
            Regex rgx = new Regex("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$");
            bool isValid = rgx.IsMatch(email);
            return isValid;
        }

        public bool EditPassword(string password, string username)
        {
            string parsedPassword = HttpUtility.HtmlDecode(password);
            if (String.IsNullOrWhiteSpace(parsedPassword) || String.IsNullOrWhiteSpace(username) || parsedPassword.Length > 50)
            {
                return false;
            }

            USER userResult = userBL.GetUser(username);
            userResult.SALT = null;
            userResult.PASSWORD = parsedPassword;
            bool editSuccess = userBL.EditUserPassword(userResult);
            return editSuccess;
        }

        public bool EditAboutMe(string username, string aboutMeContent)
        {
            string parsedContent = HttpUtility.HtmlDecode(aboutMeContent);
            if (String.IsNullOrWhiteSpace(username) || parsedContent.Length > 2000)
            {
                return false;
            }
            else
            {
                USER user = userBL.GetUser(username);
                user.ABOUT_ME = parsedContent;
                bool editSuccess = userBL.EditUser(user);
                return editSuccess;
            }
        }

        public bool EditProfilePicture(string username, HttpPostedFileBase file)
        {
            if (String.IsNullOrWhiteSpace(username) || file == null)
            {
                return false;
            }

            byte[] profilePicture = ConvertProfilePicture(file);
            USER user = userBL.GetUser(username);
            user.PROFILE_PIC = profilePicture;
            bool editSuccess = userBL.EditUser(user);
            return editSuccess;
        }

        private byte[] ConvertProfilePicture(HttpPostedFileBase file)
        {
            byte[] picByteArray = null;
            if (file != null && imageManager.IsImage(file) && imageManager.IsValidSize(file))
            {
                picByteArray = imageManager.imageToByteArray(file);
            }
            return picByteArray;
        }

        private string GenderDisplay(string gender)
        {
            string genderDisplay;
            if (gender == "M")
            {
                genderDisplay = "Male";
            }
            else if (gender == "F")
            {
                genderDisplay = "Female";
            }
            else
            {
                genderDisplay = "Unspecified";
            }
            return genderDisplay;
        }

        public List<Models.UserProfileModel> GetUsersSearchResults(string searchQuery)
        {
            string parsedQuery = HttpUtility.HtmlDecode(searchQuery);
            List<Models.UserProfileModel> searchResults = new List<Models.UserProfileModel>();
            List<USER> searchUsers = userBL.GetUserSearchResults(parsedQuery);
            foreach (var item in searchUsers)
            {
                searchResults.Add(new Models.UserProfileModel()
                {
                    FirstName = item.FIRST_NAME,
                    LastName = item.LAST_NAME,
                    Username = item.USER_NAME,
                    ProfilePic = item.PROFILE_PIC,
                });
            }
            return searchResults;
        }

        public bool IsExistingEmail(string email)
        {
            bool isExisting = false;
            string parsedEmail = HttpUtility.HtmlDecode(email);
            isExisting = userBL.DoesEmailExist(parsedEmail);
            return isExisting;
        }

        public bool IsExistingEmail(string email, string user)
        {
            bool isExisting = false;
            string parsedEmail = HttpUtility.HtmlDecode(email);
            string parsedUser = HttpUtility.HtmlDecode(user);
            isExisting = userBL.DoesEmailExist(parsedEmail);
            bool isUser = true;
            if (isExisting == true)
            {
                isUser = userBL.GetUsernameByEmail(parsedEmail).ToUpper() == parsedUser.ToUpper();
            }            
            return isExisting && !isUser;
        }

        public bool IsExistingUsername(string username)
        {
            bool isExisting = false;
            string parsedUsername = HttpUtility.HtmlDecode(username);
            isExisting = userBL.DoesUsernameExist(parsedUsername);
            return isExisting;
        }

        public bool IsExistingUsername(string username, string user)
        {
            bool isExisting = false;
            string parsedUsername = HttpUtility.HtmlDecode(username);
            string parsedUser = HttpUtility.HtmlDecode(user);
            isExisting = userBL.DoesUsernameExist(parsedUsername);
            bool isUser = parsedUsername.ToUpper() == parsedUser.ToUpper();
            return isExisting && !isUser;
        }

        public bool CheckOldPassword(string username, string oldPassword)
        {
            string parsedOldPassword = HttpUtility.HtmlDecode(oldPassword);
            bool check = false;
            check = userBL.CheckOldPassword(username, parsedOldPassword);
            return check;
        }
    }
}