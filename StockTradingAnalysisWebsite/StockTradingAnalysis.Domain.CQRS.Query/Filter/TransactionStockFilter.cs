using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Filter;

namespace StockTradingAnalysis.Domain.CQRS.Query.Filter
{
    /// <summary>
    /// The class TransactionStockFilter filters a list of transactions over the given stock.
    /// </summary>
    public class TransactionStockFilter : ITransactionFilter
    {
        /// <summary>
        /// The stock
        /// </summary>
        private readonly IStock _stock;

        /// <summary>
        /// Initialize this class with the given arguments
        /// </summary>
        /// <param name="stock"></param>
        public TransactionStockFilter(IStock stock)
        {
            _stock = stock;
        }

        /// <summary>
        /// Filters the given <paramref name="input"/> and returns all filtered <see cref="T"/>
        /// </summary>
        /// <param name="input">Input elements</param>
        /// <returns>Result after filtering</returns>
        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> input)
        {
            return input.Where(i => i.Stock.Id.Equals(_stock.Id));
        }
    }
}