using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvidenceClinic.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Appointment()
        {
            return View();
        }

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult Ecommerce()
        {
            return View();
        }

    }
}