using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Interfaces.Scheduler;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Common.Scheduler;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for schedules jobs.
	/// </summary>
	public class SchedulerBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IScheduledJob, UpdateQuotationsScheduledJob>();
		}
	}
}