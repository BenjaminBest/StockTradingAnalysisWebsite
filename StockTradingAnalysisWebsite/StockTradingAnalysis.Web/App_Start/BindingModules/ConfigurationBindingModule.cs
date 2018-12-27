using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Core.Configuration;
using StockTradingAnalysis.Interfaces.Configuration;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for configuration
	/// </summary>
	public class ConfigurationBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//Configuration Registry
			serviceCollection.AddSingleton<IConfigurationRegistry>(context =>
			{
				var registry = new ConfigurationRegistry();

				registry.AddValue("StockTradingAnalysis_MSSQL",
					DependencyResolver.Current.GetService<IConfiguration>()
						.GetConnectionString("StockTradingAnalysis_MSSQL"));

				registry.AddValue("StockTradingAnalysis_RavenDB",
					DependencyResolver.Current.GetService<IConfiguration>()
						.GetConnectionString("StockTradingAnalysis_RavenDB"));

				return registry;

			});
		}
	}
}