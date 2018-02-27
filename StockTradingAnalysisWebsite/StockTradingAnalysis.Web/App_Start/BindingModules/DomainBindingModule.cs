using Ninject.Modules;
using StockTradingAnalysis.Core.Extensions;
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
    /// <summary>
    /// Binding module for the domain
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class DomainBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Services
            Bind<IPriceCalculatorService>().To<PriceCalculatorService>().InSingletonScope();
            Bind<IStockQuoteService>().To<StockQuoteService>().InSingletonScope();
            Bind<ITransactionPerformanceService>().To<TransactionPerformanceService>().InSingletonScope();
            Bind<ITransactionBook>().To<TransactionBook>().InSingletonScope();
            Bind<IAccumulationPlanStatisticService>().To<AccumulationPlanStatisticService>().InSingletonScope();
            Bind<IInterestRateCalculatorService>().To<InterestRateCalculatorService>();
            Bind<ITransactionCalculationService>().To<TransactionCalculationService>();
            Bind<ITimeSliceCreationService>().To<TimeSliceCreationService>();
            Bind<IStatisticService>().To<StatisticService>();

            //Model repositories
            Bind<IModelRepositoryDeletionCoordinator>().To<ModelRepositoryDeletionCoordinator>();

            Bind<IModelReaderRepository<IStock>, IModelWriterRepository<IStock>, IModelRepository<IStock>, ISupportsDataDeletion>()
                .To<StockModelRepository>().InSingletonScope();

            Bind<IModelReaderRepository<IFeedback>, IModelWriterRepository<IFeedback>, IModelRepository<IFeedback>, ISupportsDataDeletion>()
                .To<FeedbackModelRepository>().InSingletonScope();

            Bind<IModelReaderRepository<ICalculation>, IModelWriterRepository<ICalculation>, IModelRepository<ICalculation>, ISupportsDataDeletion>()
                .To<CalculationModelRepository>().InSingletonScope();

            Bind<IModelReaderRepository<IStrategy>, IModelWriterRepository<IStrategy>, IModelRepository<IStrategy>, ISupportsDataDeletion>()
                .To<StrategyModelRepository>().InSingletonScope();

            Bind<IModelReaderRepository<ITransaction>, IModelWriterRepository<ITransaction>, IModelRepository<ITransaction>, ISupportsDataDeletion>()
                .To<TransactionModelRepository>().InSingletonScope();

            Bind<IModelReaderRepository<ITransactionPerformance>, IModelWriterRepository<ITransactionPerformance>, IModelRepository<ITransactionPerformance>, ISupportsDataDeletion>()
                .To<TransactionPerformanceModelRepository>().InSingletonScope();

            Bind<IModelReaderRepository<IAccountBalance>, IModelWriterRepository<IAccountBalance>, IModelRepository<IAccountBalance>, ISupportsDataDeletion>()
                .To<AccountBalanceModelRepository>().InSingletonScope();

            Bind<IModelReaderRepository<IFeedbackProportion>, IModelWriterRepository<IFeedbackProportion>, IModelRepository<IFeedbackProportion>, ISupportsDataDeletion>()
                .To<FeedbackProportionModelRepository>().InSingletonScope();

            Bind<IModelReaderRepository<IStockStatistics>, IModelWriterRepository<IStockStatistics>, IModelRepository<IStockStatistics>, ISupportsDataDeletion>()
                .To<StockStatisticsModelRepository>().InSingletonScope();

            Bind<ITimeSliceModelRepository<IStatistic>, ITimeSliceModelReaderRepository<IStatistic>, ITimeSliceModelWriterRepository<IStatistic>>()
                .To<TimeSliceModelRepository<IStatistic>>().InSingletonScope();

            //Repository
            Kernel.FindAllInterfacesOfType("StockTradingAnalysis.*.dll", typeof(IAggregateRepository<>));
        }
    }
}