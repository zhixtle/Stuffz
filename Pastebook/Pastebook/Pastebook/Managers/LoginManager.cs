using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PastebookEntities;
using PastebookBusinessLogic;

namespace Pastebook.Managers
{
    public class LoginManager
    {
        private static UserBL userBL = new UserBL();

        public bool RegisterUser(Models.UserModel user)
        {
            USER newUser = new USER()
            {
                USER_NAME = user.Username,
                PASSWORD = user.Password,
                SALT = null,
                FIRST_NAME = user.FirstName,
                LAST_NAME = user.LastName,
                BIRTHDAY = user.Birthday,
                COUNTRY_ID = user.CountryID,
                MOBILE_NO = user.MobileNumber,
                GENDER = (user.Gender == null) ? "U" : user.Gender,
                PROFILE_PIC = null,
                DATE_CREATED = DateTime.Now,
                ABOUT_ME = null,
                EMAIL_ADDRESS = user.EmailAddress
            };
            bool registerSuccess = userBL.RegisterUser(newUser);
            return registerSuccess;
        }

        public bool[] CheckUserAccount(string emailAddress, string password)
        {
            bool[] userAccountResult = new bool[] { false, false };
            string parsedEmailAddress = HttpUtility.HtmlDecode(emailAddress);
            string parsedPassword = HttpUtility.HtmlDecode(password);
            if (String.IsNullOrEmpty(parsedEmailAddress) == false && String.IsNullOrEmpty(parsedPassword) == false)
            {
                userAccountResult[0] = userBL.DoesUserExist(parsedEmailAddress);
                if (userAccountResult[0] == true)
                {
                    userAccountResult[1] = userBL.LoginUser(parsedEmailAddress, parsedPassword);
                }
                else
                {
                    userAccountResult[1] = false;
                }
            }
            return userAccountResult;
        }

        public bool CheckLoginFailed(bool[] checkResult)
        {
            bool userCheckResult = checkResult.Any(stat => stat == false);
            return userCheckResult;
        }

        public string GetUsername(string email)
        {
            return userBL.GetUsernameByEmail(email);
        }        
    }
}