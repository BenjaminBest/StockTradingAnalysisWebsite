using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Web.Common.Filter
{
    /// <summary>
    /// The class TransactionStockTypeFilter filters a list of transactions over the given stock type
    /// </summary>
    public class TransactionStockTypeFilter : ITransactionFilter
    {
        /// <summary>
        /// The stock type
        /// </summary>
        private readonly string _stockType;

        /// <summary>
        /// Initialize this class with the given arguments
        /// </summary>
        /// <param name="stockType">Type of the stock.</param>
        public TransactionStockTypeFilter(string stockType)
        {
            _stockType = stockType; ;
        }

        /// <summary>
        /// Filters the given <paramref name="input"/> and returns all filtered <see cref="T"/>
        /// </summary>
        /// <param name="input">Input elements</param>
        /// <returns>Result after filtering</returns>
        public IEnumerable<ITransaction> Apply(IEnumerable<ITransaction> input)
        {
            return !string.IsNullOrEmpty(_stockType) ? input.Where(i => i.Stock.Type.Equals(_stockType)) : input;
        }
    }
}