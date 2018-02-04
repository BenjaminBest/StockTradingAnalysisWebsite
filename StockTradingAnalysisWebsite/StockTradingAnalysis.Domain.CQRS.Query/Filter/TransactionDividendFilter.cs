using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;

namespace StockTradingAnalysis.Domain.CQRS.Query.Filter
{
    /// <summary>
    /// The class TransactionEndDateFilter filters a list of transactions based if its a dividend transaction
    /// </summary>
    public class TransactionDividendFilter : ITransactionFilter
    {
        /// <summary>
        /// The use dividends
        /// </summary>
        private readonly bool _useDividends;

        /// <summary>
        /// Initialize this class with the given arguments
        /// </summary>
        /// <param name="useDividends"><c>True</c> if dividends should be used</param>
        public TransactionDividendFilter(bool useDividends)
        {
            _useDividends = useDividends; ;
        }

        /// <summary>
        /// Filters the given <paramref name="input"/> and returns all filtered <see cref="T"/>
        /// </summary>
        /// <param name="input">Input elements</param>
        /// <returns>Result after filtering</returns>
        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> input)
        {
            return !_useDividends ? input.Where(i => !(i is IDividendTransaction)) : input;
        }
    }
}