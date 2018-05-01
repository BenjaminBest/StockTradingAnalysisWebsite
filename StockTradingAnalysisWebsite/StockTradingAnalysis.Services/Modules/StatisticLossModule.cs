using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticProfitModule is a calculation module for the statistic service to calculate losses.
    /// </summary>
    public static class StatisticLossModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        public static void CalculateLosses(this Statistic statistic, IReadOnlyCollection<ITransactionPerformance> transactions)
        {
            //statistic.LossAbsolute
            //statistic.LossAverage
            //statistic.LossAveragePercentage
            //statistic.LossMaximum          

        }
    }
}
