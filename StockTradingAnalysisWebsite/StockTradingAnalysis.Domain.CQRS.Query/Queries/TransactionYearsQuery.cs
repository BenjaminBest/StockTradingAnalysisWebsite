using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class TransactionYearsQuery : IQuery<IEnumerable<int>>
    {
        private readonly List<ITransactionFilter> _filters = new List<ITransactionFilter>();

        /// <summary>
        /// Registers the given <param name="filter">Filter</param>
        /// </summary>
        /// <typeparam name="T">A filter for transactions</typeparam>
        /// <param name="filter">Filter</param>
        /// <returns></returns>
        public TransactionYearsQuery Register<T>(T filter) where T : ITransactionFilter
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