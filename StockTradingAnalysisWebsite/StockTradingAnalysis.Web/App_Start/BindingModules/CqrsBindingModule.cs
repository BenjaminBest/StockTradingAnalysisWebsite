using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.CQRS.CommandDispatcher;
using StockTradingAnalysis.CQRS.QueryDispatcher;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common.Binding;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for CQRS
	/// </summary>
	public class CqrsBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//CQRS Queries
			serviceCollection.AddSingleton<IQueryDispatcher, QueryDispatcher>();
			serviceCollection.AddTransientForAllInterfaces(typeof(IQueryHandler<,>));

			//CQRS Commands
			serviceCollection.AddSingleton<ICommandDispatcher, CommandDispatcher>();
			serviceCollection.AddTransientForAllInterfaces(typeof(ICommandHandler<>));
		}
	}
}