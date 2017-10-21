using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Defines a selling transaction (closing)
    /// </summary>
    public class SellingTransaction : ISellingTransaction
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        public SellingTransaction(Guid id)
        {
            Id = id;
        }

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
        /// Gets/sets the taxes paid
        /// </summary>
        public decimal Taxes { get; set; }

        /// <summary>
        /// Gets/sets the minimum quote during trade
        /// </summary>
        public decimal? MAE { get; set; }

        /// <summary>
        /// Gets/sets the maximum quote during trade
        /// </summary>
        public decimal? MFE { get; set; }

        /// <summary>
        /// Gets/sets the feedbacks
        /// </summary>
        public IEnumerable<IFeedback> Feedback { get; set; }
    }
}