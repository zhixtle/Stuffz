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

        public bool LoginUser(string email, string password)
        {
            USER user = userDataAccess.GetSingle(u => u.EMAIL_ADDRESS == email);
            bool loginSucess = false;
                loginSucess = passwordBL.IsPasswordMatch(password, user.SALT, user.PASSWORD);
            return loginSucess;
        }

        public bool DoesUserExist(string email)
        {
            bool userExists = false;
            userExists = userDataAccess.GetAll().Any(u => u.EMAIL_ADDRESS == email);
            return userExists;
        }

        public USER GetUserProfile(string username)
        {
            USER user = userDataAccess.GetSingle(u => u.USER_NAME == username);
            return user;
        }

        public bool EditUser(USER user)
        {
            bool editSuccess = userDataAccess.Edit(user);
            return editSuccess;
        }

        public string GetUsernameByEmail(string email)
        {
            USER user = userDataAccess.GetSingle(u => u.EMAIL_ADDRESS == email);
            return user.USER_NAME;
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
    }
}
