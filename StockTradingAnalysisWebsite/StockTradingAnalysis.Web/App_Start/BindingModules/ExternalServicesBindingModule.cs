using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Services.External.Interfaces;
using StockTradingAnalysis.Services.External.Services;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for the external services
	/// </summary>
	public class ExternalServicesBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//Services
			serviceCollection.AddSingleton<IStockQuoteExternalService, StockQuoteExternalService>();
		}
	}
}