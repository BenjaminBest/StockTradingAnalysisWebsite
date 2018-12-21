using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Web.BootModules
{
	/// <summary>
	/// Defines an boot module for initializing the logging infrastructure
	/// </summary>
	public class LoggingBootModule : IBootModule
	{
		/// <summary>
		/// Gets the priority
		/// </summary>
		public int Priority => 10;

		/// <summary>
		/// Boots up the module
		/// </summary>
		public void Boot()
		{
			var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
			XmlConfigurator.Configure(repo);
		}
	}
}