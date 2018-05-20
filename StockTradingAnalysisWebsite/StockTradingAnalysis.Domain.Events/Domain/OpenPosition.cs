using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// The class OpenPosition contains the information about all shares which are not yet sold.
    /// </summary>
    /// <seealso cref="IOpenPosition" />
    public class OpenPosition : IOpenPosition
    {
        /// <summary>
        /// Gets the id of the product
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        /// Gets the first order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime FirstOrderDate { get; set; }

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
        /// Gets or sets the order costs.
        /// </summary>
        /// <value>
        /// The order costs.
        /// </value>
        public decimal OrderCosts { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="productId">The id of the product</param>
        public OpenPosition(Guid productId)
        {
            ProductId = productId;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{ProductId}] {Shares} * {PricePerShare} + {OrderCosts}= {PositionSize}";
        }
    }
}
