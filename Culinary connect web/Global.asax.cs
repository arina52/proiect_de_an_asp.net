using culinaryConnect.BusinessLogic.Seed;
using culinaryConnect.Web.App_Start;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace culinaryConnect.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
           BundleConfig.RegisterBundle(BundleTable.Bundles);
           Database.SetInitializer(new DbInitializer());
        }
    }
}