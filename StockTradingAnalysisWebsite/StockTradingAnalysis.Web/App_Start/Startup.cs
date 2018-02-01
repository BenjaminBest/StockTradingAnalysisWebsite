using Microsoft.Owin;
using Owin;
using StockTradingAnalysis.Web;

[assembly: OwinStartup(typeof(Startup), "Configuration")]
namespace StockTradingAnalysis.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}