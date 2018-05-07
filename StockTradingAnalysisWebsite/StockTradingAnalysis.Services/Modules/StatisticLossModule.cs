using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Services.Core;
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
        /// <param name="mathCalculatorService"></param>
        public static void CalculateLosses(this Statistic statistic,
            IReadOnlyCollection<ITransactionPerformance> transactions,
            IMathCalculatorService mathCalculatorService)
        {
            var lossTransactions = transactions.Where(t => !t.ProfitMade).ToList();

            if (!lossTransactions.Any())
                return;

            statistic.LossAbsolute = decimal.Round(lossTransactions.Sum(s => s.ProfitAbsolute), 2);
            statistic.LossAverage = decimal.Round(lossTransactions.Average(s => s.ProfitAbsolute), 2);
            statistic.LossAveragePercentage = mathCalculatorService.CalculateGeometricMean(lossTransactions.Select(t => t.ProfitPercentage));
            statistic.LossMaximum = decimal.Round(lossTransactions.Max(s => s.ProfitAbsolute), 2);
            statistic.PercentageOfLossTransactions = decimal.Round(Convert.ToDecimal(statistic.AmountOfLossTransactions / (decimal)statistic.AmountOfTransactions) * 100, 2);
        }
    }
}
