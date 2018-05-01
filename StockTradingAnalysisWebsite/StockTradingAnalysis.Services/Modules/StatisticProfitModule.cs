using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticProfitModule is a calculation module for the statistic service to calculate profits.
    /// </summary>
    public static class StatisticProfitModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        public static void CalculateProfits(this Statistic statistic, IReadOnlyCollection<ITransactionPerformance> transactions)
        {
            //statistic.ProfitAbsolute
            //statistic.ProfitAverage
            //statistic.ProfitAveragePercentage
            //statistic.ProfitMaximum
            //statistic.AbsoluteProfitPerStockName
            //statistic.AbsoluteProfitPerTradingType
            //statistic.AbsoluteProfitPerWeekDay
            //statistic.AmountOfProfitTransactions
            //statistic.PercentageOfProfitTransactions     

        }
    }
}
