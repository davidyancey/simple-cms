using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using AgileDevDays.Core.Authentication.OAuth;
using AgileDevDays.Core.Authentication.Twitter;
using AgileDevDays.Core.Common;
using AgileDevDays.Core.Enumerations;
using AgileDevDays.Core.Membership;
using Common = AgileDevDays.Core.Authentication.Twitter.Common;

namespace AgileDevDays.Web.Controllers
{
    public class AuthController : BaseController
    {
        private readonly Manager _twitterManager;

        public AuthController()
        {
            if(_twitterManager == null)
                _twitterManager = new Manager();
        }

        public ActionResult Login()
        {
            ResponseToken token = _twitterManager.GetRequestToken(Common.ConsumerKey, Common.ConsumerSecret,
                                                                 "http://www.agiledevdays.com/dv/Auth/Twitter");

            ViewBag.TwitterAuthUri = _twitterManager.BuildAuthorizationUri(token.Token, true);
            return View();
        }

        public ActionResult Twitter()
        {
            ResponseToken token = _twitterManager.GetAccessTokenDuringCallback(Common.ConsumerKey, Common.ConsumerSecret);
            string username = token.ScreenName;

            MembershipUser user = Membership.GetUser(username);
            if(user == null)
            {
               RegisterUser(username, ApplicationSource.Twitter);
               return RedirectToAction("Register", "Account");
            }
            ValidateUser(username);
            return RedirectToAction("Index", "Home");
        }

        private void RegisterUser(string username, ApplicationSource authenticationSource)
        {
            Session["UserName"] = username;
            var user = Membership.CreateUser(username, Constants.Password);
            var userID = user.ProviderUserKey.ToString();

            var manager = new MembershipManager();
            manager.UpdateMemberAuthenticationSource(userID, authenticationSource);

            FormsAuthentication.SetAuthCookie(username, false);
            FormsAuthentication.RedirectFromLoginPage(username, false);
        }

        private void ValidateUser(string username)
        {
            if (Membership.ValidateUser(username, Constants.Password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                FormsAuthentication.RedirectFromLoginPage(username, false);
            }
        }
    }
}
