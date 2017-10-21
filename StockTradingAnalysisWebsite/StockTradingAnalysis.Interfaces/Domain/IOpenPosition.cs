using System;

namespace StockTradingAnalysis.Interfaces.Domain
{
    public interface IOpenPosition
    {
        /// <summary>
        /// Gets the id of the product
        /// </summary>
        Guid ProductId { get; }

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