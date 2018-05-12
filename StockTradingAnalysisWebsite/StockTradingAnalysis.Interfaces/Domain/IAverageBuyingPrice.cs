using System;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IAverageBuyingPrice defines the information about the average price for a specific date.
    /// </summary>
    public interface IAverageBuyingPrice
    {
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        DateTime OrderDate { get; }

        /// <summary>
        /// Gets the average price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        decimal AveragePrice { get; }
    }
}