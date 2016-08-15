using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace instinct360.Controllers
{
    [Authorize]
    public class SecureController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Secure Home Page";

            return View();
        }
    }
}
