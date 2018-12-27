using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Core.Services;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Common.ItemResolvers;
using StockTradingAnalysis.Web.Common.Services;
using ImageService = StockTradingAnalysis.Web.Common.Services.ImageService;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for common classes
	/// </summary>
	public class CommonBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//Services
			serviceCollection.AddTransient<ILoggingService, LoggingService>();
			serviceCollection.AddSingleton<IImageService, ImageService>();
			serviceCollection.AddSingleton<IMathCalculatorService, MathCalculatorService>();
			serviceCollection.AddSingleton<IDateCalculationService, DateCalculationService>();
			serviceCollection.AddSingleton<ISerializerService, JsonSerializerService>();

			//Selectlist item resolvers
			serviceCollection.AddSingleton<ISelectItemResolver, StockTypeItemResolver>();
			serviceCollection.AddSingleton<ISelectItemResolver, TimePeriodAbsoluteItemResolver>();
			serviceCollection.AddSingleton<ISelectItemResolver, TimePeriodRelativeItemResolver>();
			serviceCollection.AddSingleton<ISelectItemResolver, StockItemResolver>();
			serviceCollection.AddSingleton<ISelectItemResolver, FeedbackItemResolver>();
			serviceCollection.AddSingleton<ISelectItemResolver, StrategyItemResolver>();

			serviceCollection.AddSingleton<ISelectItemResolverRegistry, ItemResolverRegistry>();

			serviceCollection.AddSingleton<IQuotationServiceClient, QuotationServiceClient>();
		}
	}
}