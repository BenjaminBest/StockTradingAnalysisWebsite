using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Domain.Process.Finders;
using StockTradingAnalysis.Domain.Process.Locator;
using StockTradingAnalysis.Domain.Process.ProcessManagers;
using StockTradingAnalysis.Domain.Process.Repository;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Web.Common.Binding;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for process managers.
	/// </summary>
	public class ProcessBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransientForAllInterfaces(typeof(IStartedByMessage<>));
			serviceCollection.AddTransientForAllInterfaces(typeof(IMessageCorrelationIdCreator<>));

			serviceCollection.AddSingleton<IProcessManagerRepository, ProcessManagerRepository>();
			serviceCollection.AddSingleton<IProcessManagerCoordinator, ProcessManagerCoordinator>();
			serviceCollection.AddSingleton<IProcessManagerFinderRepository, ProcessManagerFinderRepository>();

			serviceCollection.AddTransient<IProcessManager, TransactionProcessManager>();
			serviceCollection.AddTransient<IProcessManager, StatisticsProcessManager>();
		}
	}
}