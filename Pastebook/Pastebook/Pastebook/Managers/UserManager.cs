﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;

namespace Pastebook.Managers
{
    public class UserManager
    {
        private static UserBL userBL = new UserBL();
        private static FriendBL friendBL = new FriendBL();
        private static CountryBL countryBL = new CountryBL();

        
        public Models.UserProfileModel GetUserProfile (string username, string profileUsername)
        {
            USER userResult = userBL.GetUserProfile(profileUsername);
            Models.UserProfileModel user = new Models.UserProfileModel()
            {
                AboutMe = userResult.ABOUT_ME,
                Birthday = userResult.BIRTHDAY,
                Country = countryBL.GetCountryName(userResult.COUNTRY_ID),
                EmailAddress = userResult.EMAIL_ADDRESS,
                FirstName = userResult.FIRST_NAME,
                Gender = GenderDisplay(userResult.GENDER),
                LastName = userResult.LAST_NAME,
                MobileNumber = userResult.MOBILE_NO,
                ProfilePic = userResult.PROFILE_PIC,
                Username = userResult.USER_NAME,
                IsFriend = friendBL.IsUserAFriend(username, profileUsername)
            };
            return user;
        }

        public Models.UserModel GetUser(string username)
        {
            USER userResult = userBL.GetUserProfile(username);
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
            USER userResult = userBL.GetUserProfile(username);
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

        public bool EditEmail(string email, string username)
        {
            string parsedEmail = HttpUtility.HtmlDecode(email);
            USER userResult = userBL.GetUserProfile(username);
            userResult.EMAIL_ADDRESS = parsedEmail;
            bool editSuccess = userBL.EditUserEmail(userResult);
            return editSuccess;
        }

        public bool EditPassword(string password, string username)
        {
            string parsedPassword = HttpUtility.HtmlDecode(password);
            USER userResult = userBL.GetUserProfile(username);
            userResult.SALT = null;
            userResult.PASSWORD = parsedPassword;
            bool editSuccess = userBL.EditUserPassword(userResult);
            return editSuccess;
        }

        public bool EditAboutMe(string username, string aboutMeContent)
        {
            string parsedContent = HttpUtility.HtmlDecode(aboutMeContent);
            if (parsedContent.Length > 2000)
            {
                return false;
            }
            else
            {
                USER user = userBL.GetUserProfile(username);
                user.ABOUT_ME = parsedContent;
                bool editSuccess = userBL.EditUser(user);
                return editSuccess;
            }
        }

        public bool EditProfilePicture(string username, byte[] profilePicture)
        {
            USER user = userBL.GetUserProfile(username);
            user.PROFILE_PIC = profilePicture;
            bool editSuccess = userBL.EditUser(user);
            return editSuccess;
        }

        public string GenderDisplay(string gender)
        {
            string genderDisplay;
            if(gender == "M")
            {
                genderDisplay = "Male";
            }
            else if(gender == "F")
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
                    ProfilePic = item.PROFILE_PIC,
                    Username = item.USER_NAME
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
            bool isUser = userBL.GetUsernameByEmail(parsedEmail) == parsedUser;
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
            bool isUser = parsedUsername == parsedUser;
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