using StockTradingAnalysis.Core.Performance;
using StockTradingAnalysis.Core.Services;
using StockTradingAnalysis.Interfaces.Enumerations;

namespace StockTradingAnalysis.Core.Tests.Mocks
{
    public class PerformanceCounterMock
    {
        public static PerformanceMeasurementService GetMock()
        {
            var registry = new PerformanceRegistry();

            registry.Register<PerformanceCounterNumberOfItems>(PerformanceType.NumberOfItems);
            registry.Register<PerformanceCounterRatePerSecond>(PerformanceType.RateOfCountsPerMillisecond);
            registry.Register<PerformanceCounterAverageTimer>(PerformanceType.AverageTimer);

            return new PerformanceMeasurementService(registry);
        }
    }
}