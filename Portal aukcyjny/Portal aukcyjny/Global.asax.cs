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
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    public class Global : HttpApplication
    {
        Product[] products = new Product[]
{
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
};

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