using Ninject.Extensions.Conventions;
using Ninject.Modules;
using StockTradingAnalysis.Domain.CQRS.Query.ReadModel;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Services.Services;

namespace StockTradingAnalysis.Web.BindingModules
{
    public class DomainBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Services
            Kernel.Bind<IPriceCalculatorService>().To<PriceCalculatorService>().InSingletonScope();
            Kernel.Bind<IStockQuoteService>().To<StockQuoteService>().InSingletonScope();
            Kernel.Bind<ITransactionPerformanceService>().To<TransactionPerformanceService>().InSingletonScope();
            Kernel.Bind<ITransactionBook>().To<TransactionBook>().InSingletonScope();
            Kernel.Bind<IAccumulationPlanStatisticService>().To<AccumulationPlanStatisticService>().InSingletonScope();
            Kernel.Bind<IInterestRateCalculatorService>().To<InterestRateCalculatorService>().InSingletonScope();

            //Model repositories
            Kernel.Bind<IModelReaderRepository<IStock>, IModelWriterRepository<IStock>, IModelRepository<IStock>>()
                .To<StockModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IFeedback>, IModelWriterRepository<IFeedback>, IModelRepository<IFeedback>>()
                .To<FeedbackModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<ICalculation>, IModelWriterRepository<ICalculation>, IModelRepository<ICalculation>>()
                .To<CalculationModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IStrategy>, IModelWriterRepository<IStrategy>, IModelRepository<IStrategy>>()
                .To<StrategyModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<ITransaction>, IModelWriterRepository<ITransaction>, IModelRepository<ITransaction>>()
                .To<TransactionModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<ITransactionPerformance>, IModelWriterRepository<ITransactionPerformance>, IModelRepository<ITransactionPerformance>>()
                .To<TransactionPerformanceModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IAccountBalance>, IModelWriterRepository<IAccountBalance>, IModelRepository<IAccountBalance>>()
                .To<AccountBalanceModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IFeedbackProportion>, IModelWriterRepository<IFeedbackProportion>, IModelRepository<IFeedbackProportion>>()
                .To<FeedbackProportionModelRepository>().InSingletonScope();

            //Repository
            Kernel.Bind(i => i
                .FromAssembliesMatching("StockTradingAnalysis.*.dll")
                .SelectAllClasses()
                .InheritedFrom(typeof(IAggregateRepository<>))
                .BindAllInterfaces()
                );
        }
    }
}