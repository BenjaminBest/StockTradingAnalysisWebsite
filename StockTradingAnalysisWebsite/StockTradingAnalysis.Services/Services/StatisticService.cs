using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Domain;
using StockTradingAnalysis.Services.Modules;

namespace StockTradingAnalysis.Services.Services
{
    /// <summary>
    /// The class StatisticService defines a service to calculate time range based statistical information for transational data.
    /// </summary>
    public class StatisticService : IStatisticService
    {
        /// <summary>
        /// The transactions
        /// </summary>
        private readonly IReadOnlyList<ITransaction> _transactions;

        /// <summary>
        /// The transactions
        /// </summary>
        private readonly IReadOnlyList<IFeedback> _feedback;

        /// <summary>
        /// The transactions
        /// </summary>
        private readonly IReadOnlyList<ITransactionPerformance> _transactionPerformances;

        /// <summary>
        /// The feedback proportions
        /// </summary>
        private readonly IReadOnlyList<IFeedbackProportion> _feedbackProportions;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticService"/> class.
        /// </summary>
        public StatisticService(IQueryDispatcher queryDispatcher)
        {
            _transactions = new ReadOnlyCollection<ITransaction>(queryDispatcher.Execute(new TransactionAllQuery()).ToList());
            _feedback = new ReadOnlyCollection<IFeedback>(queryDispatcher.Execute(new FeedbackAllQuery()).ToList());
            _transactionPerformances = new ReadOnlyCollection<ITransactionPerformance>(queryDispatcher.Execute(new TransactionPerformanceAllQuery()).ToList());
            _feedbackProportions = new ReadOnlyCollection<IFeedbackProportion>(queryDispatcher.Execute(new FeedbackProportionAllQuery()).ToList());
        }

        /// <summary>
        /// Starts calculation and uses all transactions
        /// </summary>
        /// <param name="timeRange">The time slice which represents a time range.</param>
        /// <returns></returns>
        public IStatistic Calculate(ITimeSlice timeRange)
        {
            if (_transactions == null)
                return null;

            if (!_transactions.Any())
                return null;

            var result = new Statistic(timeRange);

            var transactions = FilterTransactions(_transactions, timeRange);
            var transactionPerformances = FilterPerformances(_transactionPerformances, transactions);

            //TODO:Filter all the others here based on transaction or date

            result.CalculateCosts(transactions);
            result.CalculateFeedback(_feedback, _feedbackProportions);//TODO: Feedback filtern nach performance/transactions
            result.CalculateAmounts(transactionPerformances);
            result.CalculateProfits(transactionPerformances);
            result.CalculateLosses(transactionPerformances);
            result.CalculateAssetClass(transactionPerformances);

            return result;
        }

        /// <summary>
        /// Filters the transaction performances based on the given <param name="transactions"></param> to that only the performance values for the given
        /// transactions are returned.
        /// </summary>
        /// <param name="transactionPerformances">The transaction performances.</param>
        /// <param name="transactions">The transactions.</param>
        /// <returns></returns>
        private static IReadOnlyList<ITransactionPerformance> FilterPerformances(
            IEnumerable<ITransactionPerformance> transactionPerformances, IEnumerable<ITransaction> transactions)
        {
            var result = from t in transactions
                         join tp in transactionPerformances on t.Id equals tp.Id
                         select tp;

            return result.ToList();
        }

        /// <summary>
        /// Filters the transactions.
        /// </summary>
        /// <param name="transactions">The transactions.</param>
        /// <param name="timeRange">The time range.</param>
        /// <returns></returns>
        private static IReadOnlyList<ITransaction> FilterTransactions(IEnumerable<ITransaction> transactions, ITimeSlice timeRange)
        {
            return transactions.Where(t => t.OrderDate >= timeRange.Start && t.OrderDate <= timeRange.End).ToList();
        }
    }
}
