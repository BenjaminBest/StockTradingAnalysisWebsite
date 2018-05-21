namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IDetailedOpenPositionSummary defines aggregated information for all open positions.
    /// </summary>
    public interface IDetailedOpenPositionSummary
    {
        /// <summary>
        /// Gets the position size (with order costs)
        /// </summary>
        decimal PositionSize { get; }

        /// <summary>
        /// Gets the order costs.
        /// </summary>
        /// <value>
        /// The order costs.
        /// </value>
        decimal OrderCosts { get; }

        /// <summary>
        /// Gets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        IProfit Profit { get; }
    }
}