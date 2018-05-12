namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IProfit defines the result of a transaction in terms of if profit was made.
    /// </summary>
    public interface IProfit
    {
        /// <summary>
        /// Gets the absolute profit
        /// </summary>
        decimal ProfitAbsolute { get; }

        /// <summary>
        /// Gets the profit (in %)
        /// </summary>
        decimal ProfitPercentage { get; }

        /// <summary>
        /// Gets <c>true</c> if profit was made
        /// </summary>
        bool ProfitMade { get; }
    }
}