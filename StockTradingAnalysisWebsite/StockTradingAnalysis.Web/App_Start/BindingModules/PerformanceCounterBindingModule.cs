using Ninject.Modules;
using StockTradingAnalysis.Core.Performance;
using StockTradingAnalysis.Core.Services;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Enumerations;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Web.BindingModules
{
    public class PerformanceCounterBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Performance counter-templates
            Kernel.Bind<IPerformanceRegistry>().ToMethod((context) =>
            {
                var registry = new PerformanceRegistry();

                registry.Register<PerformanceCounterNumberOfItems>(PerformanceType.NumberOfItems);
                registry.Register<PerformanceCounterRatePerSecond>(PerformanceType.RateOfCountsPerMillisecond);
                registry.Register<PerformanceCounterAverageTimer>(PerformanceType.AverageTimer);

                return registry;

            }).InSingletonScope();

            //Service
            Kernel.Bind<IPerformanceMeasurementService>().To<PerformanceMeasurementService>().InSingletonScope();
        }
    }
}