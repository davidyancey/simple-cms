using System;
using System.Web.Mvc;
using System.Web.Security;
using AgileDevDays.Web.Models;

namespace AgileDevDays.Web.Controllers
{
    public class AccountController : BaseController
    {

        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            var username = User.Identity.Name ?? (string)Session["UserName"];
            var model = new RegisterModel {UserName = username};
            return View(model);
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MembershipUser user = Membership.GetUser(model.UserName);
                    user.Email = model.Email;
                    Membership.UpdateUser(user);

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    FormsAuthentication.RedirectFromLoginPage(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Registration Error", ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
