using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Defines a dividend transaction
    /// </summary>
    public class DividendTransaction : IDividendTransaction
    {
        /// <summary>
        /// Gets/sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the version
        /// </summary>
        public int OriginalVersion { get; set; }

        /// <summary>
        /// Gets/sets the order date
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets/sets the amount of shares
        /// </summary>
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets/sets the price per share
        /// </summary>
        public decimal PricePerShare { get; set; }

        /// <summary>
        /// Gets/sets the order costs
        /// </summary>
        public decimal OrderCosts { get; set; }

        /// <summary>
        /// Gets/sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets/sets the tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets/sets the image
        /// </summary>
        public IImage Image { get; set; }

        /// <summary>
        /// Gets/sets the underlying stock
        /// </summary>
        public IStock Stock { get; set; }

        /// <summary>
        /// Gets/sets the position size (amount of money put in the trade, without transaction costs)
        /// </summary>
        public decimal PositionSize { get; set; }

        /// <summary>
        /// Gets/sets a transaction description if this was a buy, sell or dividend
        /// </summary>        
        public string Action { get; set; }

        /// <summary>
        /// Gets/sets the taxes paid
        /// </summary>
        public decimal Taxes { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        public DividendTransaction(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Dividend [{Stock.Name}] {Shares} * {PricePerShare} on {OrderDate}";
        }
    }
}