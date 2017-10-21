using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.Web.Migration.Common
{
    public class PerformanceStats
    {
        public static void Reset()
        {
            var performanceMeasurementService = DependencyResolver.GetService<IPerformanceMeasurementService>();

            performanceMeasurementService.Reset();
        }

        public static void WriteToConsole()
        {
            var performanceMeasurementService = DependencyResolver.GetService<IPerformanceMeasurementService>();
            var loggingService = DependencyResolver.GetService<ILoggingService>();


            var results = performanceMeasurementService.GetResults();

            foreach (var result in results)
            {
                loggingService.Info(result.Display);
            }
        }
    }
}