using System;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Web.BootModules;

namespace StockTradingAnalysis.Web.Migration
{
	public static class Initialize
	{
		/// <summary>
		/// Initializes the application
		/// </summary>
		public static void Start()
		{
			//Booting configuration modules
			new LoggingBootModule().Boot();
			new AutoMapperBootModule().Boot();

			//Booting configuration modules
			new ProcessManagerLocatorBootModule().Boot();

			//Erase Database
			DependencyResolver.Current.GetService<IModelRepositoryDeletionCoordinator>().DeleteAll();
		}

		/// <summary>
		/// Returns the logging service
		/// </summary>
		/// <returns></returns>
		public static ILoggingService GetLogger()
		{
			return DependencyResolver.Current.GetService<ILoggingService>();
		}

		public static void Stop()
		{
		}
	}
}