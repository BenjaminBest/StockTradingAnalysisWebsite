using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Core.Performance;
using StockTradingAnalysis.Core.Services;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Enumerations;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for performance counters.
	/// </summary>
	public class PerformanceCounterBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//Performance counter-templates
			serviceCollection.AddSingleton<IPerformanceRegistry>(context =>
			{
				var registry = new PerformanceRegistry();

				registry.Register<PerformanceCounterNumberOfItems>(PerformanceType.NumberOfItems);
				registry.Register<PerformanceCounterRatePerSecond>(PerformanceType.RateOfCountsPerMillisecond);
				registry.Register<PerformanceCounterAverageTimer>(PerformanceType.AverageTimer);

				return registry;

			});

			//Service
			serviceCollection.AddSingleton<IPerformanceMeasurementService, PerformanceMeasurementService>();
		}
	}
}