using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
namespace SimpleCms.Web.Controllers
{
    public class SetupController : Controller
    {
        //
        // GET: /Setup/

        public ActionResult Setup()
        {
            ViewBag.DbConfiguration = String.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionName"]) ?
                "/Images/Setup/notok.png" : "/Images/Setup/ok.png";
            return View();
        }

    }
}
