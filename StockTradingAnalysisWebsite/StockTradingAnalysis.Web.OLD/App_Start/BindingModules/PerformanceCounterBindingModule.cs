using Ninject.Modules;
using StockTradingAnalysis.Core.Performance;
using StockTradingAnalysis.Core.Services;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Enumerations;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for performance counters.
	/// </summary>
	/// <seealso cref="NinjectModule" />
	public class PerformanceCounterBindingModule : NinjectModule
	{
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		public override void Load()
		{
			//Performance counter-templates
			Bind<IPerformanceRegistry>().ToMethod((context) =>
			{
				var registry = new PerformanceRegistry();

				registry.Register<PerformanceCounterNumberOfItems>(PerformanceType.NumberOfItems);
				registry.Register<PerformanceCounterRatePerSecond>(PerformanceType.RateOfCountsPerMillisecond);
				registry.Register<PerformanceCounterAverageTimer>(PerformanceType.AverageTimer);

				return registry;

			}).InSingletonScope();

			//Service
			Bind<IPerformanceMeasurementService>().To<PerformanceMeasurementService>().InSingletonScope();
		}
	}
}