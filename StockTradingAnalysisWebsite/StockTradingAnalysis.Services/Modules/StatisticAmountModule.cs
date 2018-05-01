using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticAmountModule is a calculation module for the statistic service to amounts of special characteristics.
    /// </summary>
    public static class StatisticAmountModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        public static void CalculateAmounts(this Statistic statistic, IReadOnlyCollection<ITransactionPerformance> transactions)
        {
            //statistic.AmountPositionTrades   
            //statistic.AmountIntradayTrades
            //statistic.AmountOfLossTransactions =
            //statistic.AmountOfProfitTransactions
            //statistic.AmountOfTransactions
            //statistic.AmountOfTransactionsPerMonth
            //statistic.AmountOfTransactionsPerWeek
            //statistic.AmountOfTransactionsPerYear         
        }
    }
}
