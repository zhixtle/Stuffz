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

        public ActionResult Login()
        {
            ViewData["Countries"] = countriesList;
            return View();
        }

        //PREVENT HTML INJECTION

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

        //CHECK IF USERNAME OR EMAIL ALREADY EXISTS

        public async Task<ActionResult> ValidateRegistration([Bind(Include = "Username, FirstName, LastName, EmailAddress, Password, ConfirmPassword, Birthday, CountryID, MobileNumber, Gender")]Models.UserModel model)
        {
            if (ModelState.IsValid)
            {
                Managers.LoginManager loginManager = new Managers.LoginManager();
                bool registerSuccess = loginManager.RegisterUser(model);

                //mail sending tutorial source: http://www.mikesdotnetting.com/article/268/how-to-send-email-in-asp-net-mvc

                var message = new MailMessage()
                {
                    From = new MailAddress("lizbeth.oliman.pastebook@gmail.com"),
                    Subject = "Pastebook Validation",
                    Body = "This is your validation message from Pastebook. Welcome to Pastebook!",
                    IsBodyHtml = true
                };
                message.To.Add(new MailAddress(model.EmailAddress));
                
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                return View(model);
            }
            else
            {
                ViewData["Countries"] = countriesList;
                return RedirectToAction("Login", model);
            }
        }

        public JsonResult CheckOldPassword(string oldPassword)
        {
            Managers.LoginManager loginManager = new Managers.LoginManager();
            bool correctOldPassword = loginManager.CheckOldPassword(Session["user"].ToString(), oldPassword);
            return Json(new { Status = correctOldPassword }, JsonRequestBehavior.AllowGet);
        }

        //MAKE THESE INTO JSON RESULTS AND DISPLAY SUCCESS OR FAIL

        public ActionResult EditDetails(Models.UserModel model)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool editSuccess = userManager.EditUser(model, Session["user"].ToString());
            ViewData["Countries"] = countriesList;
            return RedirectToAction("Settings");
        }

        public ActionResult EditEmail(string email)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool editSuccess = userManager.EditEmail(email, Session["user"].ToString());
            ViewData["Countries"] = countriesList;
            return PartialView("Settings", "Home");
        }

        public ActionResult EditPassword(string password)
        {
            Managers.UserManager userManager = new Managers.UserManager();
            bool editSuccess = userManager.EditPassword(password, Session["user"].ToString());
            ViewData["Countries"] = countriesList;
            return PartialView("Settings", "Home");
        }
    }
}