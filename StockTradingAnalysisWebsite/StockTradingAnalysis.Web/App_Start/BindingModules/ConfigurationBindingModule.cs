using System.Configuration;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Core.Configuration;
using StockTradingAnalysis.Interfaces.Configuration;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for configuration
	/// </summary>
	/// <seealso cref="NinjectModule" />
	public class ConfigurationBindingModule : NinjectModule
	{
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		public override void Load()
		{
			//Configuration Registry
			Bind<IConfigurationRegistry>().ToMethod(context =>
			{
				var registry = new ConfigurationRegistry();

				registry.AddValue("StockTradingAnalysis_MSSQL", DependencyResolver.GetService<IConfiguration>().GetConnectionString("StockTradingAnalysis_MSSQL"));
				registry.AddValue("StockTradingAnalysis_RavenDB", DependencyResolver.GetService<IConfiguration>().GetConnectionString("StockTradingAnalysis_RavenDB"));

				return registry;

			}).InSingletonScope();
		}
	}
}