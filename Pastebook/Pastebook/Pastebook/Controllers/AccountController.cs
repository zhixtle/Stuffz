using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class AccountController : Controller
    {
        private static Managers.CountryManager countryManager = new Managers.CountryManager();
        private static List<Models.CountryModel> countriesList = countryManager.GetCountriesList();

        [Route("login")]
        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Countries"] = countriesList;
            return View();
        }

        public JsonResult UserLogin(string email, string password)
        {
            Managers.LoginManager loginManager = new Managers.LoginManager();
            bool[] userLoginStatus = loginManager.CheckUserAccount(email, password);
            bool loginSuccess = loginManager.CheckLoginSuccess(userLoginStatus);
            if (loginSuccess == true)
            {
                Session["user"] = loginManager.GetUsername(email);
            }
            return Json(new { UserExists = userLoginStatus[0], PasswordMatch = userLoginStatus[1] }, JsonRequestBehavior.AllowGet);
        }

        [Route("validate")]
        public ActionResult ValidateRegistration(Models.UserModel model)
        {
            if (ModelState.IsValid)
            {
                Managers.LoginManager loginManager = new Managers.LoginManager();
                bool registerSuccess = loginManager.RegisterUser(model);

                //var message = new MailMessage()
                //{
                //    From = new MailAddress("lizbeth.oliman.pastebook@gmail.com"),
                //    Subject = "Pastebook Validation",
                //    Body = "This is your validation message from Pastebook. Welcome to Pastebook!",
                //    IsBodyHtml = true
                //};
                //message.To.Add(new MailAddress(model.EmailAddress));

                //using (var smtp = new SmtpClient())
                //{
                //    await smtp.SendMailAsync(message);
                //}

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", model);
            }
        }

        public JsonResult CheckOldPassword(string oldPassword)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool correctOldPassword = userManager.CheckOldPassword(Session["user"].ToString(), oldPassword);
            return Json(new { Status = correctOldPassword }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditDetails([Bind(Include = "Username, FirstName, LastName, Birthday, CountryID, MobileNumber, Gender")]
                                         Models.UserModel model)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool editSuccess = userManager.EditUser(model, Session["user"].ToString());
            TempData["saveDetails"] = editSuccess;
            if (editSuccess)
            {
                Session["user"] = model.Username;
            }
            return RedirectToAction("Settings", "Account");
        }

        public JsonResult EditEmail(string email)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool editSuccess = userManager.EditEmail(email, Session["user"].ToString());
            return Json(new { Status = editSuccess }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditPassword(string password)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool editSuccess = userManager.EditPassword(password, Session["user"].ToString());
            return Json(new { Status = editSuccess }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckExistingEmail(string email)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool isExisting = userManager.IsExistingEmail(email, Session["user"].ToString());
            return Json(new { Status = isExisting }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckExistingUsername(string username)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool isExisting = userManager.IsExistingUsername(username, Session["user"].ToString());
            return Json(new { Status = isExisting }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckExistingUsernameEmail(string username, string email)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool isExistingUsername = userManager.IsExistingUsername(username);
            bool isExistingEmail = userManager.IsExistingEmail(email);
            return Json(new { UserExists = isExistingUsername, EmailExists = isExistingEmail }, JsonRequestBehavior.AllowGet);
        }

        [Route("settings")]
        public ActionResult Settings()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Managers.UserManager userManager = new Managers.UserManager();
            Models.UserModel model = userManager.GetUser(Session["user"].ToString());
            ViewData["Countries"] = countriesList;
            ViewData["saveDetails"] = TempData["saveDetails"];
            return View(model);
        }

        [Route("logout")]
        public ActionResult UserLogout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}