using Ss.RealEstate.Library2;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ss.RealEstate.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception == null) return;

            SmtpMailer.SendMail("HomeScorer@HomeScorer.us", new string[] { "avangari@gmail.com" }, "Exception on HomeScorer", exception.StackTrace); 

            // Clear the error
            Server.ClearError();

            // Redirect to a landing page
            Response.Redirect("~/views/Error/Index");
        }
    }
}
