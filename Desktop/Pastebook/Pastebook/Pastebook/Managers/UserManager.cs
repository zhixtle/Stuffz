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

        
        public Models.UserProfileModel GetUserProfile (string username, string profileUsername)
        {
            USER userResult = userBL.GetUserProfile(profileUsername);
            Models.UserProfileModel user = new Models.UserProfileModel()
            {
                AboutMe = userResult.ABOUT_ME,
                Birthday = userResult.BIRTHDAY,
                CountryID = userResult.COUNTRY_ID,
                EmailAddress = userResult.EMAIL_ADDRESS,
                FirstName = userResult.FIRST_NAME,
                Gender = userResult.GENDER,
                LastName = userResult.LAST_NAME,
                MobileNumber = userResult.MOBILE_NO,
                ProfilePic = userResult.PROFILE_PIC,
                Username = userResult.USER_NAME,
                IsFriend = friendBL.IsUserAFriend(username, profileUsername)
            };
            return user;
        }
    }
}