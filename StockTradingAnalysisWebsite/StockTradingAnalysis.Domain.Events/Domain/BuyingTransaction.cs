using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Defines a buying transaction (opening)
    /// </summary>
    public class BuyingTransaction : IBuyingTransaction
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
        /// Gets/sets the amount of units
        /// </summary>
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets/sets the price per unit
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
        /// Gets/sets the initial stop loss
        /// </summary>
        public decimal InitialSL { get; set; }

        /// <summary>
        /// Gets/sets the initial take profit
        /// </summary>
        public decimal InitialTP { get; set; }

        /// <summary>
        /// Gets/sets the strategy used
        /// </summary>
        public IStrategy Strategy { get; set; }

        /// <summary>
        /// Gets/sets the change risk ratio = TP Points/SL Points
        /// </summary>
        public decimal CRV { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        public BuyingTransaction(Guid id)
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
            return $"Buy [{Stock.Name}] {Shares} * {PricePerShare} on {OrderDate}";
        }
    }
}