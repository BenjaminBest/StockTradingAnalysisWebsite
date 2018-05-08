using System;
using System.Collections.Generic;
using System.Linq;
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
            var range = statistic.End - statistic.Start;

            statistic.AmountOfLossTransactions = transactions.Count(t => !t.ProfitMade);
            statistic.AmountOfProfitTransactions = transactions.Count(t => t.ProfitMade);
            statistic.AmountOfTransactions = transactions.Count;
            statistic.AmountOfTransactionsPerYear = decimal.Round(Convert.ToDecimal(statistic.AmountOfTransactions / (range.TotalDays / 365)), 2);
            statistic.AmountOfTransactionsPerMonth = decimal.Round(Convert.ToDecimal(statistic.AmountOfTransactions / (range.TotalDays / 30)), 2);
            statistic.AmountOfTransactionsPerWeek = decimal.Round(Convert.ToDecimal(statistic.AmountOfTransactions / (range.TotalDays / 7)), 2);


            var positionTrades = transactions.Where(t => t.HoldingPeriod != null && !t.HoldingPeriod.IsIntradayTrade && t.HoldingPeriod.HasValue()).ToList();
            var intradayTrades = transactions.Where(t => t.HoldingPeriod != null && t.HoldingPeriod.IsIntradayTrade && t.HoldingPeriod.HasValue()).ToList();

            statistic.AmountPositionTrades = positionTrades.Count;
            statistic.AmountIntradayTrades = intradayTrades.Count;

            if (statistic.AmountIntradayTrades != 0)
                statistic.AvgHoldingPeriodIntradayTrades = intradayTrades.Average(t => t.HoldingPeriod.ToMinutes());

            if (statistic.AmountPositionTrades != 0)
                statistic.AvgHoldingPeriodPositionTrades = positionTrades.Average(t => t.HoldingPeriod.ToDays());
        }
    }
}
