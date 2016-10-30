using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pastebook.Models;
using Pastebook.Managers;
using PastebookBusinessLogic;


namespace PastebookUnitTest
{
    [TestClass]
    public class LoginManagerUnitTest
    {

        [TestMethod]
        public void CheckUserAccountUnitTest()
        {
            LoginManager manager = new LoginManager();
            bool[] result = manager.CheckUserAccount("test@email.com", "pass");
            bool allTrue = result[0] == result[1] == true;
            Assert.IsTrue(allTrue);
        }

        [TestMethod]
        public void CheckLoginFailedUnitTest()
        {
            LoginManager manager = new LoginManager();
            bool[] parameter = { true, true };
            bool result = manager.CheckLoginFailed(parameter);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetUsernameUnitTest()
        {
            LoginManager manager = new LoginManager();
            string result = manager.GetUsername("test@email.com");
            Assert.AreEqual("test", result);
        }

        [TestMethod]
        public void DoesUserExistUnitTest()
        {
            UserBL userBL = new UserBL();
            bool result = userBL.DoesUserExist("test@email.com");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoginUserUnitTest()
        {
            UserBL userBL = new UserBL();
            bool result = userBL.LoginUser("test@email.com", "pass");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DoesUserExist()
        {
            PasswordBL passwordBL = new PasswordBL();
            bool result = passwordBL.IsPasswordMatch("pass", "o:?6???n! 2??=#?{??\\?", "??=??qa????????8?4??s=z?Q");
            Assert.IsTrue(result);
        }

    }
}
