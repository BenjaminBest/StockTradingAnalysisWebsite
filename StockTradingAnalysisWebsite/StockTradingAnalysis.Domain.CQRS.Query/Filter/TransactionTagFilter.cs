using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;

namespace StockTradingAnalysis.Domain.CQRS.Query.Filter
{
    /// <summary>
    /// The class TransactionEndDateFilter filters a list of transactions over the given tag
    /// </summary>
    public class TransactionTagFilter : ITransactionFilter
    {
        private readonly string _tag;

        /// <summary>
        /// Initialize this class with the given arguments
        /// </summary>
        /// <param name="tag">Tag</param>
        public TransactionTagFilter(string tag)
        {
            _tag = tag;
        }

        /// <summary>
        /// Filters the given <paramref name="input"/> and returns all filtered <see cref="T"/>
        /// </summary>
        /// <param name="input">Input elements</param>
        /// <returns>Result after filtering</returns>
        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> input)
        {
            return input.Where(i => i.Tag != null && i.Tag.Equals(_tag));
        }
    }
}