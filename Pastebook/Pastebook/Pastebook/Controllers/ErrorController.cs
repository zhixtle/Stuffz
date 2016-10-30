using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastebook.Controllers
{
    public class ErrorController : Controller
    {
        [Route("oops/{id:int}")]
        public ActionResult Oops(int id)
        {
            Response.StatusCode = id;
            return View();
        }
    }
}