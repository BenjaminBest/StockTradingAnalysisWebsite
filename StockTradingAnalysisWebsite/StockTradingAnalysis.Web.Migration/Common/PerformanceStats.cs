using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Web.Migration.Common
{
	public class PerformanceStats
	{
		public static void Reset()
		{
			var performanceMeasurementService = DependencyResolver.Current.GetService<IPerformanceMeasurementService>();

			performanceMeasurementService.Reset();
		}

		public static void WriteToConsole()
		{
			var performanceMeasurementService = DependencyResolver.Current.GetService<IPerformanceMeasurementService>();
			var loggingService = DependencyResolver.Current.GetService<ILoggingService>();


			var results = performanceMeasurementService.GetResults();

			foreach (var result in results)
			{
				loggingService.Info(result.Display);
			}
		}
	}
}