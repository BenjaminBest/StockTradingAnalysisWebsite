using System;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IAggregatedOpenPosition defines aggregated information for all open positions of a security. 
    /// In contrary to <see cref="IOpenPosition"/> it contains average prices etc., so all buyings are combined.
    /// </summary>
    public interface IDetailedOpenPosition
    {
        /// <summary>
        /// Gets the stock
        /// </summary>
        IStock Stock { get; }

        /// <summary>
        /// Gets the first order date.
        /// </summary>
        /// <value>
        /// The first order date.
        /// </value>
        DateTime FirstOrderDate { get; }

        /// <summary>
        /// Gets the amount of units
        /// </summary>
        decimal Shares { get; }

        /// <summary>
        /// Gets the price per unit
        /// </summary>
        decimal AveragePricePerShare { get; }

        /// <summary>
        /// Gets the position size (with order costs)
        /// </summary>
        decimal PositionSize { get; }

        /// <summary>
        /// Gets the current quotation.
        /// </summary>
        /// <value>
        /// The current quotation.
        /// </value>
        IQuotation CurrentQuotation { get; }

        /// <summary>
        /// Gets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        IProfit Profit { get; }
    }
}