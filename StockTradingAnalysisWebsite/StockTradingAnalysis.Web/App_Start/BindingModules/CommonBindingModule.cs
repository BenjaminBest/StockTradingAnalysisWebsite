using Ninject.Modules;
using StockTradingAnalysis.Core.Services;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Common.ItemResolvers;
using StockTradingAnalysis.Web.Common.Services;

namespace StockTradingAnalysis.Web.BindingModules
{
    /// <summary>
    /// Binding module for common classes
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class CommonBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Services
            Bind<IDependencyService>().To<DependencyService>().InSingletonScope();
            Bind<ILoggingService>().To<LoggingService>();
            Bind<IImageService>().To<ImageService>().InSingletonScope();
            Bind<IMathCalculatorService>().To<MathCalculatorService>().InSingletonScope();
            Bind<IDateCalculationService>().To<DateCalculationService>().InSingletonScope();
            Bind<ISerializerService>().To<JsonSerializerService>().InSingletonScope();

            //Selectlist item resolvers
            Bind<ISelectItemResolver>().To<StockTypeItemResolver>().InSingletonScope();
            Bind<ISelectItemResolver>().To<TimePeriodAbsoluteItemResolver>().InSingletonScope();
            Bind<ISelectItemResolver>().To<TimePeriodRelativeItemResolver>().InSingletonScope();
            Bind<ISelectItemResolver>().To<StockItemResolver>().InSingletonScope();
            Bind<ISelectItemResolver>().To<FeedbackItemResolver>().InSingletonScope();
            Bind<ISelectItemResolver>().To<StrategyItemResolver>().InSingletonScope();

            Bind<ISelectItemResolverRegistry>().To<ItemResolverRegistry>().InSingletonScope();

            Bind<IQuotationServiceClient>().To<QuotationServiceClient>().InSingletonScope();
        }
    }
}