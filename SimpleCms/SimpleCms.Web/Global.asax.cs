using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SimpleCms.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            VerifySetup(RouteTable.Routes);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        private void VerifySetup(RouteCollection routes)
        {
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["ApplicationId"].ToString()))
            {
                routes.MapRoute(
                name: "Setup",
                url: "{controller}/{action}/",
                defaults: new { controller = "Setup", action = "Setup" }
                );
            }
        }
    }
}