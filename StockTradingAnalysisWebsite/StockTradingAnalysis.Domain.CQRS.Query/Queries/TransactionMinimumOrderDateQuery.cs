using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The TransactionMinimumOrderDateQuery is used to return the order date of the oldest transaction.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{DateTime}" />
    public class TransactionMinimumOrderDateQuery : IQuery<DateTime>
    {
        /// <summary>
        /// The filters
        /// </summary>
        private readonly List<ITransactionFilter> _filters = new List<ITransactionFilter>();

        /// <summary>
        /// Registers the given <param name="filter">Filter</param>
        /// </summary>
        /// <typeparam name="T">A filter for transactions</typeparam>
        /// <param name="filter">Filter</param>
        /// <returns></returns>
        public TransactionMinimumOrderDateQuery Register<T>(T filter) where T : ITransactionFilter
        {
            _filters.Add(filter);

            return this;
        }

        /// <summary>
        /// Apply filters on all transactions
        /// </summary>
        public IEnumerable<ITransaction> Filter(IEnumerable<ITransaction> transactions)
        {
            return _filters.Aggregate(transactions, (current, filter) => filter.Apply(current));
        }
    }
}