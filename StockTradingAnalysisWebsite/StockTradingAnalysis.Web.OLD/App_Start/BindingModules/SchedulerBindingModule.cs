using Ninject.Modules;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Scheduler;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Common.ItemResolvers;
using StockTradingAnalysis.Web.Common.Scheduler;
using StockTradingAnalysis.Web.Common.Services;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for schedules jobs.
	/// </summary>
	/// <seealso cref="NinjectModule" />
	public class SchedulerBindingModule : NinjectModule
	{
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		public override void Load()
		{
			Bind<IScheduledJob>().To<UpdateQuotationsScheduledJob>();
		}
	}
}