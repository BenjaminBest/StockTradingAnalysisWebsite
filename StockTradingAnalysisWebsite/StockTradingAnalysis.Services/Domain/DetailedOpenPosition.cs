using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The DetailedOpenPosition hold the information about a open position per stock. All open transactions are included.
    /// </summary>
    /// <seealso cref="IDetailedOpenPosition" />
    public class DetailedOpenPosition : IDetailedOpenPosition
    {
        /// <summary>
        /// Gets the stock
        /// </summary>
        public IStock Stock { get; set; }

        /// <summary>
        /// Gets the first order date.
        /// </summary>
        /// <value>
        /// The first order date.
        /// </value>
        public DateTime FirstOrderDate { get; set; }

        /// <summary>
        /// Gets the amount of units
        /// </summary>
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets the price per unit
        /// </summary>
        public decimal AveragePricePerShare { get; set; }

        /// <summary>
        /// Gets the position size (with order costs)
        /// </summary>
        public decimal PositionSize { get; set; }

        /// <summary>
        /// Gets the order costs.
        /// </summary>
        /// <value>
        /// The order costs.
        /// </value>
        public decimal OrderCosts { get; set; }

        /// <summary>
        /// Gets the current quotation.
        /// </summary>
        /// <value>
        /// The current quotation.
        /// </value>
        public IQuotation CurrentQuotation { get; set; }

        /// <summary>
        /// Gets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        public IProfit Profit { get; set; }

        /// <summary>
        /// Gets or sets the current years profit.
        /// </summary>
        /// <value>
        /// The current year profit.
        /// </value>
        public IProfit YearToDateProfit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedOpenPosition"/> class.
        /// </summary>
        /// <param name="stock">The stock.</param>
        public DetailedOpenPosition(IStock stock)
        {
            Stock = stock;
        }
    }
}
