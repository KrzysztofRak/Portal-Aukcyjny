using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Portal_aukcyjny
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static string GetDefaultCurrency()
        {
            if (HttpContext.Current.Request.Cookies["defaultCurrency"] != null)
                return HttpContext.Current.Request.Cookies["defaultCurrency"].Value.ToString();
            else
                return "pln";
        }

        public static string GetDefaultCultureCode()
        {
            if (HttpContext.Current.Request.Cookies["defaultCultureInfo"] != null)
                return HttpContext.Current.Request.Cookies["defaultCultureInfo"].Value.ToString();
            else
                return "pl-PL";
        }
    }
}