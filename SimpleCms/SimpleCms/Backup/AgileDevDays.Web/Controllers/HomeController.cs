using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileDevDays.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Agile Dev Days";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
