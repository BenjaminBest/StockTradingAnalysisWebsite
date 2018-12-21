using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//Booting configuration modules
			var bootModules = DependencyResolver.GetServices<IBootModule>()
				.OrderByDescending(m => m.Priority);
			var bootManager = new BootManager(bootModules);
			bootManager.Boot();

			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseApplicationInsights();
	}
}
