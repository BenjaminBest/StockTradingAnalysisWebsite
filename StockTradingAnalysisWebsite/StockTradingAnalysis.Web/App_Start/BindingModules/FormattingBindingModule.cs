using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Web.Common.Binding;
using StockTradingAnalysis.Web.Common.Formatting;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for frontend formatting
	/// </summary>
	public class FormattingBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//Repository
			serviceCollection.AddSingleton<IStatisticCardConverterRepository, StatisticCardConverterRepository>();

			//Converters
			serviceCollection.AddTransientForAllInterfaces(typeof(IStatisticCardConverter));
		}
	}
}