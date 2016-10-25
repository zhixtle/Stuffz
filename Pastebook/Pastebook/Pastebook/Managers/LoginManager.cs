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
            userAccountResult[0] = userBL.DoesUserExist(emailAddress);
            if (userAccountResult[0] == true)
            {
                userAccountResult[1] = userBL.LoginUser(emailAddress, password);
            }
            else
            {
                userAccountResult[1] = false;
            }
            return userAccountResult;
        }

        public string GetUsername(string email)
        {
            return userBL.GetUsernameByEmail(email);
        }

        public bool CheckOldPassword(string username, string oldPassword)
        {
            bool check = false;
            check = userBL.CheckOldPassword(username, oldPassword);
            return check;
        }
    }
}