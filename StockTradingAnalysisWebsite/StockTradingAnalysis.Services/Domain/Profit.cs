using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The Profit defines the information about a transaction result in terms of profit.
    /// </summary>
    /// <seealso cref="IProfit" />
    public class Profit : IProfit
    {
        /// <summary>
        /// Gets the absolute profit
        /// </summary>
        public decimal ProfitAbsolute { get; set; }

        /// <summary>
        /// Gets the profit (in %)
        /// </summary>
        public decimal ProfitPercentage { get; set; }

        /// <summary>
        /// Gets <c>true</c> if profit was made
        /// </summary>
        public bool ProfitMade { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Profit"/> class.
        /// </summary>
        /// <param name="profitAbsolute">The profit absolute.</param>
        /// <param name="profitPercentage">The profit percentage.</param>
        public Profit(decimal profitAbsolute, decimal profitPercentage)
        {
            ProfitAbsolute = profitAbsolute;
            ProfitPercentage = profitPercentage;
            ProfitMade = profitAbsolute > 0;
        }
    }
}
