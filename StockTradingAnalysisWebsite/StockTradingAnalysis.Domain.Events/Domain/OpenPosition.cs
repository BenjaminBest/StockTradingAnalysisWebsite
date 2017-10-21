using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    public class OpenPosition : IOpenPosition
    {
        /// <summary>
        /// Gets the id of the product
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        /// Gets the amount of units
        /// </summary>
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets the price per unit
        /// </summary>
        public decimal PricePerShare { get; set; }

        /// <summary>
        /// Gets the position size (with order costs)
        /// </summary>
        public decimal PositionSize { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="productId">The id of the product</param>
        public OpenPosition(Guid productId)
        {
            ProductId = productId;
        }
    }
}
