using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Domain.CQRS.Query.ReadModel;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.EventSourcing.DomainContext;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Services;
using StockTradingAnalysis.Web.Common.Deletion;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for the domain
	/// </summary>
	public class DomainBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//Services
			serviceCollection.AddSingleton<IPriceCalculatorService, PriceCalculatorService>();
			serviceCollection.AddSingleton<IStockQuoteService, StockQuoteService>();
			serviceCollection.AddSingleton<ITransactionPerformanceService, TransactionPerformanceService>();
			serviceCollection.AddSingleton<IAccumulationPlanStatisticService, AccumulationPlanStatisticService>();
			serviceCollection.AddTransient<IInterestRateCalculatorService, InterestRateCalculatorService>();
			serviceCollection.AddTransient<ITransactionCalculationService, TransactionCalculationService>();
			serviceCollection.AddTransient<ITimeSliceCreationService, TimeSliceCreationService>();
			serviceCollection.AddTransient<IStatisticService, StatisticService>();

			var transactionBook = new TransactionBook();

			serviceCollection.AddSingleton<ITransactionBook>(s=> transactionBook);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => transactionBook);

			//Model repositories
			serviceCollection.AddTransient<IModelRepositoryDeletionCoordinator, ModelRepositoryDeletionCoordinator>();

			var stockModelRepository = new StockModelRepository();
			serviceCollection.AddSingleton<IModelRepository<IStock>>(s => stockModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<IStock>>(s => stockModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<IStock>>(s => stockModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => stockModelRepository);

			var feedbackModelRepository = new FeedbackModelRepository();
			serviceCollection.AddSingleton<IModelRepository<IFeedback>>(s => feedbackModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<IFeedback>>(s => feedbackModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<IFeedback>>(s => feedbackModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => feedbackModelRepository);

			var calculationModelRepository = new CalculationModelRepository();
			serviceCollection.AddSingleton<IModelRepository<ICalculation>>(s => calculationModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<ICalculation>>(s => calculationModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<ICalculation>>(s => calculationModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => calculationModelRepository);

			var strategyModelRepository = new StrategyModelRepository();
			serviceCollection.AddSingleton<IModelRepository<IStrategy>>(s => strategyModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<IStrategy>>(s => strategyModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<IStrategy>>(s => strategyModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => strategyModelRepository);

			var transactionModelRepository = new TransactionModelRepository();
			serviceCollection.AddSingleton<IModelRepository<ITransaction>>(s => transactionModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<ITransaction>>(s => transactionModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<ITransaction>>(s => transactionModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => transactionModelRepository);

			var transactionPerformanceModelRepository = new TransactionPerformanceModelRepository();
			serviceCollection.AddSingleton<IModelRepository<ITransactionPerformance>>(s => transactionPerformanceModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<ITransactionPerformance>>(s => transactionPerformanceModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<ITransactionPerformance>>(s => transactionPerformanceModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => transactionPerformanceModelRepository);

			var accountBalanceModelRepository = new AccountBalanceModelRepository();
			serviceCollection.AddSingleton<IModelRepository<IAccountBalance>>(s => accountBalanceModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<IAccountBalance>>(s => accountBalanceModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<IAccountBalance>>(s => accountBalanceModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => accountBalanceModelRepository);

			var feedbackProportionModelRepository = new FeedbackProportionModelRepository();
			serviceCollection.AddSingleton<IModelRepository<IFeedbackProportion>>(s => feedbackProportionModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<IFeedbackProportion>>(s => feedbackProportionModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<IFeedbackProportion>>(s => feedbackProportionModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => feedbackProportionModelRepository);

			var stockStatisticsModelRepository = new StockStatisticsModelRepository();
			serviceCollection.AddSingleton<IModelRepository<IStockStatistics>>(s => stockStatisticsModelRepository);
			serviceCollection.AddSingleton<IModelReaderRepository<IStockStatistics>>(s => stockStatisticsModelRepository);
			serviceCollection.AddSingleton<IModelWriterRepository<IStockStatistics>>(s => stockStatisticsModelRepository);
			serviceCollection.AddSingleton<ISupportsDataDeletion>(s => stockStatisticsModelRepository);

			serviceCollection
				.AddSingleton<ITimeSliceModelRepository<IStatistic>, TimeSliceModelRepository<IStatistic>>();
			serviceCollection.AddSingleton<ITimeSliceModelReaderRepository<IStatistic>>(s =>
				s.GetService<ITimeSliceModelRepository<IStatistic>>());
			serviceCollection.AddSingleton<ITimeSliceModelWriterRepository<IStatistic>>(s =>
				s.GetService<ITimeSliceModelRepository<IStatistic>>());

			//Aggregates Repositories
			foreach (var type in TypeHelper.FindNonAbstractTypes("StockTradingAnalysis.", typeof(IAggregateRoot)))
			{
				serviceCollection.AddTransient(typeof(IAggregateRepository<>).MakeGenericType(type), typeof(AggregateRepository<>).MakeGenericType(type));
			}
		}
	}
}