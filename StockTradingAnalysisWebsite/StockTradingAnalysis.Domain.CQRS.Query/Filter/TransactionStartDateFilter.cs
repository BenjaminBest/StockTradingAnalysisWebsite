using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;

namespace StockTradingAnalysis.Domain.CQRS.Query.Filter
{
    /// <summary>
    /// The class TransactionStartDateFilter filters a list of transactions over the given startdate
    /// </summary>
    public class TransactionStartDateFilter : ITransactionFilter
    {
        private readonly DateTime _startdate;

        /// <summary>
        /// Initialize this class with the given arguments
        /// </summary>
        /// <param name="startdate">Startdate</param>
        public TransactionStartDateFilter(DateTime startdate)
        {
            _startdate = startdate;
        }

        /// <summary>
        /// Filters the given <paramref name="input"/> and returns all filtered <see cref="T"/>
        /// </summary>
        /// <param name="input">Input elements</param>
        /// <returns>Result after filtering</returns>
        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> input)
        {
            return input.Where(i => i.OrderDate >= _startdate);
        }
    }
}