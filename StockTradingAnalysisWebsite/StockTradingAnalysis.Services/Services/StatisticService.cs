using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Services
{
    /// <summary>
    /// The class StatisticService defines a service to calculate time range based statistical information for transational data
    /// </summary>
    public class StatisticService : IStatisticService
    {
        /// <summary>
        /// The transactions
        /// </summary>
        private readonly IReadOnlyList<ITransaction> _transactions;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticService"/> class.
        /// </summary>
        public StatisticService(IQueryDispatcher queryDispatcher)
        {
            _transactions = new ReadOnlyCollection<ITransaction>(queryDispatcher.Execute(new TransactionAllQuery()).ToList());
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

            return new Statistic(new AllTimeSlice(DateTime.MinValue, DateTime.MaxValue)); //TODO: Calculate, but put every calculation in a seperate class!
        }
    }
}
