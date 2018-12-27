using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Web.Common.Binding;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for boot modules
	/// </summary>
	public class BootModulesBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransientForAllInterfaces<IBootModule>();
		}
	}
}