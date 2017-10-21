using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Filter
{
    /// <summary>
    /// The interface ITransactionFilter defines a chainable filter-configuration for filtering a list of <see cref="T"/>
    /// </summary>
    public interface IFilter<T>
    {
        /// <summary>
        /// Filters the given <paramref name="input"/> and returns all filtered <see cref="T"/>
        /// </summary>
        /// <param name="input">Input elements</param>
        /// <returns>Result after filtering</returns>
        IEnumerable<T> Apply(IEnumerable<T> input);
    }
}