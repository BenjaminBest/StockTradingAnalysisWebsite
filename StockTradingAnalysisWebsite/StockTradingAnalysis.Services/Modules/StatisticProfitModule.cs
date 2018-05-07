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
    /// The StatisticProfitModule is a calculation module for the statistic service to calculate profits.
    /// </summary>
    public static class StatisticProfitModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        /// <param name="mathCalculatorService"></param>
        public static void CalculateProfits(this Statistic statistic,
            IReadOnlyCollection<ITransactionPerformance> transactions,
            IMathCalculatorService mathCalculatorService)
        {
            var profitTransactions = transactions.Where(t => t.ProfitMade).ToList();

            if (!profitTransactions.Any())
                return;

            statistic.ProfitAbsolute = decimal.Round(profitTransactions.Sum(s => s.ProfitAbsolute), 2);
            statistic.ProfitAverage = decimal.Round(profitTransactions.Average(s => s.ProfitAbsolute), 2);
            statistic.ProfitAveragePercentage = mathCalculatorService.CalculateGeometricMean(profitTransactions.Select(t => t.ProfitPercentage));
            statistic.ProfitMaximum = decimal.Round(profitTransactions.Max(s => s.ProfitAbsolute), 2);
            statistic.PercentageOfProfitTransactions = decimal.Round(Convert.ToDecimal(statistic.AmountOfProfitTransactions / (decimal)statistic.AmountOfTransactions) * 100, 2);
        }
    }
}
