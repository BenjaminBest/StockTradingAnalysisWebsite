using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;

namespace StockTradingAnalysis.Domain.CQRS.Query.Filter
{
    /// <summary>
    /// The class TransactionEndDateFilter filters a list of transactions over the given enddate
    /// </summary>
    public class TransactionEndDateFilter : ITransactionFilter
    {
        private readonly DateTime _enddate;

        /// <summary>
        /// Initialize this class with the given arguments
        /// </summary>
        /// <param name="enddate">Enddate</param>
        public TransactionEndDateFilter(DateTime enddate)
        {
            _enddate = enddate;
        }

        /// <summary>
        /// Filters the given <paramref name="input"/> and returns all filtered <see cref="T"/>
        /// </summary>
        /// <param name="input">Input elements</param>
        /// <returns>Result after filtering</returns>
        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> input)
        {
            return input.Where(i => i.OrderDate <= _enddate);
        }
    }
}