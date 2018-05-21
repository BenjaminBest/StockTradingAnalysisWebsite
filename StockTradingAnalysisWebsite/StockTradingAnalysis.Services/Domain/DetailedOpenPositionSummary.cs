using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The DetailedOpenPositionSummary holds the summary information about all open positions
    /// </summary>
    /// <seealso cref="IDetailedOpenPositionSummary" />
    public class DetailedOpenPositionSummary : IDetailedOpenPositionSummary
    {
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
        /// Gets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        public IProfit Profit { get; set; }
    }
}
