using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class LoginRegisterController : Controller
    {
        private static Managers.CountryManager countryManager = new Managers.CountryManager();
        private static List<Models.CountryModel> countriesList = countryManager.GetCountriesList();

        public ActionResult Login()
        {
            ViewData["Countries"] = countriesList;
            return View();
        }

        public JsonResult UserLogin(string email, string password)
        {
            Managers.LoginManager loginManager = new Managers.LoginManager();
            bool[] userLoginStatus = { false, false };
            if (String.IsNullOrEmpty(email) == false && String.IsNullOrEmpty(password) == false)
            {
                userLoginStatus = loginManager.CheckUserAccount(email, password);
            }
            bool loginFailed = userLoginStatus.Any(stat => stat == false);
            if (loginFailed == false)
            {
                Session["user"] = loginManager.GetUsername(email);
            }
            return Json(new { UserExists = userLoginStatus[0], PasswordMatch = userLoginStatus[1] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateRegistration([Bind(Include = "Username, FirstName, LastName, EmailAddress, Password, ConfirmPassword, Birthday, CountryID, MobileNumber, Gender")]Models.UserModel model)
        {
            if (ModelState.IsValid)
            {
                Managers.LoginManager loginManager = new Managers.LoginManager();
                bool registerSuccess = loginManager.RegisterUser(model);
                return View(model);
            }
            else
            {
                ViewData["Countries"] = countriesList;
                return RedirectToAction("Login", model);
            }
        }
    }
}