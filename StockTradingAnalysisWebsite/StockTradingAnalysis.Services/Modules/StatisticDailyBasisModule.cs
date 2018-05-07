using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticDailyBasisModule is a calculation module for the statistic service to calculate values on a daily basis
    /// </summary>
    public static class StatisticDailyBasisModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        public static void CalculateDailyBasis(this Statistic statistic, IReadOnlyCollection<ITransactionPerformance> transactions)
        {
            if (transactions.Count == 0)
                return;

            //statistic.AbsoluteProfitPerWeekDay
        }
    }
}
