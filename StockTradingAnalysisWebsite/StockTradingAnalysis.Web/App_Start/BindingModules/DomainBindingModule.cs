using Ninject.Extensions.Conventions;
using Ninject.Modules;
using StockTradingAnalysis.Domain.CQRS.Query.ReadModel;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Services;
using StockTradingAnalysis.Web.Common.Deletion;

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
            Kernel.Bind<IInterestRateCalculatorService>().To<InterestRateCalculatorService>();
            Kernel.Bind<ITransactionCalculationService>().To<TransactionCalculationService>();

            //Model repositories
            Kernel.Bind<IModelRepositoryDeletionCoordinator>().To<ModelRepositoryDeletionCoordinator>();

            Kernel.Bind<IModelReaderRepository<IStock>, IModelWriterRepository<IStock>, IModelRepository<IStock>, IModelRepositorySupportsDataDeletion>()
                .To<StockModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IFeedback>, IModelWriterRepository<IFeedback>, IModelRepository<IFeedback>, IModelRepositorySupportsDataDeletion>()
                .To<FeedbackModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<ICalculation>, IModelWriterRepository<ICalculation>, IModelRepository<ICalculation>, IModelRepositorySupportsDataDeletion>()
                .To<CalculationModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IStrategy>, IModelWriterRepository<IStrategy>, IModelRepository<IStrategy>, IModelRepositorySupportsDataDeletion>()
                .To<StrategyModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<ITransaction>, IModelWriterRepository<ITransaction>, IModelRepository<ITransaction>, IModelRepositorySupportsDataDeletion>()
                .To<TransactionModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<ITransactionPerformance>, IModelWriterRepository<ITransactionPerformance>, IModelRepository<ITransactionPerformance>, IModelRepositorySupportsDataDeletion>()
                .To<TransactionPerformanceModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IAccountBalance>, IModelWriterRepository<IAccountBalance>, IModelRepository<IAccountBalance>, IModelRepositorySupportsDataDeletion>()
                .To<AccountBalanceModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IFeedbackProportion>, IModelWriterRepository<IFeedbackProportion>, IModelRepository<IFeedbackProportion>, IModelRepositorySupportsDataDeletion>()
                .To<FeedbackProportionModelRepository>().InSingletonScope();

            Kernel.Bind<IModelReaderRepository<IStockStatistics>, IModelWriterRepository<IStockStatistics>, IModelRepository<IStockStatistics>, IModelRepositorySupportsDataDeletion>()
                .To<StockStatisticsModelRepository>().InSingletonScope();

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