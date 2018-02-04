using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class TransactionAllQuery : IQuery<IEnumerable<ITransaction>>
    {
        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <value>
        /// The filters.
        /// </value>
        public List<ITransactionFilter> Filters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionAllQuery"/> class.
        /// </summary>
        public TransactionAllQuery()
        {
            Filters = new List<ITransactionFilter>();
        }

        /// <summary>
        /// Registers the given <param name="filter">Filter</param>
        /// </summary>
        /// <typeparam name="T">A filter for transactions</typeparam>
        /// <param name="filter">Filter</param>
        /// <returns></returns>
        public TransactionAllQuery Register<T>(T filter) where T : ITransactionFilter
        {
            Filters.Add(filter);

            return this;
        }

        /// <summary>
        /// Apply filters on all transactions
        /// </summary>
        public IEnumerable<ITransaction> Filter(IEnumerable<ITransaction> transactions)
        {
            return Filters.Aggregate(transactions, (current, filter) => filter.Apply(current));
        }

        /// <summary>
        /// Resets the filters.
        /// </summary>
        public void ClearFilters()
        {
            Filters.Clear();
        }

        /// <summary>
        /// Copies the filters from the given instance <paramref name="query" />
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public TransactionAllQuery CopyFiltersFrom(TransactionAllQuery query)
        {
            foreach (var filter in query.Filters)
            {
                Register(filter);
            }

            return this;
        }
    }
}