using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookDataAccess;
using PastebookEntities;

namespace PastebookBusinessLogic
{
    public class UserBL
    {
        private static PasswordBL passwordBL = new PasswordBL();
        private static DataAccess<USER> userDataAccess = new DataAccess<USER>();

        public bool RegisterUser(USER user)
        {
            string salt = null;
            string passwordHash = passwordBL.GeneratePasswordHash(user.PASSWORD, out salt);
            user.SALT = salt;
            user.PASSWORD = passwordHash;
            bool registerSuccess = userDataAccess.Add(user);
            return registerSuccess;
        }

        public bool EditUserEmail(USER user)
        {
            bool editSuccess = userDataAccess.Edit(user);
            return editSuccess;
        }

        public bool EditUserPassword(USER user)
        {
            string salt = null;
            string passwordHash = passwordBL.GeneratePasswordHash(user.PASSWORD, out salt);
            user.SALT = salt;
            user.PASSWORD = passwordHash;
            bool editSuccess = userDataAccess.Edit(user);
            return editSuccess;
        }

        public bool LoginUser(string email, string password)
        {
            USER user = userDataAccess.GetSingle(u => u.EMAIL_ADDRESS == email);
            bool loginSucess = false;
            loginSucess = passwordBL.IsPasswordMatch(password, user.SALT, user.PASSWORD);
            return loginSucess;
        }

        public bool CheckOldPassword(string username, string oldPassword)
        {
            USER user = userDataAccess.GetSingle(u => u.USER_NAME == username);
            bool passwordCorrect = false;
            passwordCorrect = passwordBL.IsPasswordMatch(oldPassword, user.SALT, user.PASSWORD);
            return passwordCorrect;
        }

        public bool DoesUserExist(string email)
        {
            bool userExists = false;
            userExists = userDataAccess.GetAll().Any(u => u.EMAIL_ADDRESS == email);
            return userExists;
        }

        public bool DoesEmailExist(string email)
        {
            bool userExists = false;
            userExists = userDataAccess.GetAll().Any(u => u.EMAIL_ADDRESS == email);
            return userExists;
        }

        public bool DoesUsernameExist(string username)
        {
            bool userExists = false;
            userExists = userDataAccess.GetAll().Any(u => u.USER_NAME == username);
            return userExists;
        }

        public USER GetUserProfile(string username)
        {
            USER user = userDataAccess.GetSingle(u => u.USER_NAME == username);
            return user;
        }

        public List<USER> GetUserSearchResults(string searchQuery)
        {
            List<USER> userResults = userDataAccess.GetSelected(u => u.FIRST_NAME.ToUpper().Contains(searchQuery.ToUpper()) ||
                                                                     u.LAST_NAME.ToUpper().Contains(searchQuery.ToUpper()) ||
                                                                     (u.FIRST_NAME.ToUpper()+" "+u.LAST_NAME.ToUpper()).Contains(searchQuery.ToUpper()));
            return userResults;
        }

        public bool EditUser(USER user)
        {
            bool editSuccess = userDataAccess.Edit(user);
            return editSuccess;
        }

        public string GetUsernameByEmail(string email)
        {
            USER user = userDataAccess.GetSingle(u => u.EMAIL_ADDRESS == email);
            if (user != null)
            {
                return user.USER_NAME;
            }
            else
            {
                return null;
            }
        }

        public string GetUsernameByID(int id)
        {
            USER user = userDataAccess.GetSingle(u => u.ID == id);
            return user.USER_NAME;
        }

        public int GetIDByUsername(string username)
        {
            int id = userDataAccess.GetSingle(u => u.USER_NAME == username).ID;
            return id;
        }

        public string GetUserByID(int userID)
        {
            USER user = userDataAccess.GetSingle(u => u.ID == userID);
            string fullName = user.FIRST_NAME + " " + user.LAST_NAME;
            return fullName;
        }

        public byte[] GetUserProfilePicture(int userID)
        {
            return userDataAccess.GetSingle(u => u.ID == userID).PROFILE_PIC;
        }
    }
}
