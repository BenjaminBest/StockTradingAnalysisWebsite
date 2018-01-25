using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using DependencyResolver = System.Web.Mvc.DependencyResolver;

namespace StockTradingAnalysis.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Booting configuration modules
            var bootModules = DependencyResolver.Current.GetServices<IBootModule>()
                .OrderByDescending(m => m.Priority);
            var bootManager = new BootManager(bootModules);
            bootManager.Boot();
        }

        void Application_Error(object sender, EventArgs e)
        {
            var loggingService = DependencyResolver.Current.GetService<ILoggingService>();
            var exception = Server.GetLastError();

            loggingService.Fatal(exception);
        }
    }
}