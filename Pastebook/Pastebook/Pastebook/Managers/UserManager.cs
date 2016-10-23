using System;
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
                CountryID = userResult.COUNTRY_ID,
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

        public bool EditAboutMe(string username, string aboutMeContent)
        {
            USER user = userBL.GetUserProfile(username);
            user.ABOUT_ME = aboutMeContent;
            bool editSuccess = userBL.EditUser(user);
            return editSuccess;
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
    }
}