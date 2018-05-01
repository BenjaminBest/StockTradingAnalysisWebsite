using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticProfitModule is a calculation module for the statistic service to calculate statistics in regards to asset classes.
    /// </summary>
    public static class StatisticAssetClassModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        public static void CalculateAssetClass(this Statistic statistic, IReadOnlyCollection<ITransactionPerformance> transactions)
        {
            //statistic.BestAssetClassProfit
            //statistic.BestAssetProfit           

        }
    }
}
