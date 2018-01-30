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
    public class CommonBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Services
            Kernel.Bind<IDependencyService>().To<DependencyService>().InSingletonScope();
            Kernel.Bind<ILoggingService>().To<LoggingService>();
            Kernel.Bind<IImageService>().To<ImageService>().InSingletonScope();
            Kernel.Bind<IMathCalculatorService>().To<MathCalculatorService>().InSingletonScope();
            Kernel.Bind<IDateCalculationService>().To<DateCalculationService>().InSingletonScope();
            Kernel.Bind<ISerializerService>().To<JsonSerializerService>().InSingletonScope();

            //Selectlist item resolvers
            Kernel.Bind<ISelectItemResolver>().To<StockTypeItemResolver>().InSingletonScope();
            Kernel.Bind<ISelectItemResolver>().To<TimePeriodAbsoluteItemResolver>().InSingletonScope();
            Kernel.Bind<ISelectItemResolver>().To<TimePeriodRelativeItemResolver>().InSingletonScope();
            Kernel.Bind<ISelectItemResolver>().To<StockItemResolver>().InSingletonScope();
            Kernel.Bind<ISelectItemResolver>().To<FeedbackItemResolver>().InSingletonScope();
            Kernel.Bind<ISelectItemResolver>().To<StrategyItemResolver>().InSingletonScope();

            Kernel.Bind<ISelectItemResolverRegistry>().To<ItemResolverRegistry>().InSingletonScope();

            Kernel.Bind<IQuotationServiceClient>().To<QuotationServiceClient>().InSingletonScope();
        }
    }
}