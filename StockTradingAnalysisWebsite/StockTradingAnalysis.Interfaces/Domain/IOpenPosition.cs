using System;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IOpenPosition defines the open position of a security.
    /// </summary>
    public interface IOpenPosition
    {
        /// <summary>
        /// Gets the id of the product
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// Gets the first order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        DateTime FirstOrderDate { get; set; }

        /// <summary>
        /// Gets the amount of units
        /// </summary>
        decimal Shares { get; set; }

        /// <summary>
        /// Gets the price per unit
        /// </summary>
        decimal PricePerShare { get; set; }

        /// <summary>
        /// Gets the position size (with order costs)
        /// </summary>
        decimal PositionSize { get; set; }
    }
}