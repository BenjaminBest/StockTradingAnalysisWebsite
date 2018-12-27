using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StockTradingAnalysis.Web.Common.Converters;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for json converters.
	/// </summary>
	public class JsonConverterBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//Services
			serviceCollection.AddTransient<JsonConverter, QuotationJsonConverter>();
		}
	}
}