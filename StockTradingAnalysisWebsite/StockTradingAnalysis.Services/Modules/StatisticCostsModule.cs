using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticCostsModule is a calculation module for the statistic service to calculate costs.
    /// </summary>
    public static class StatisticCostsModule
    {
        /// <summary>
        /// Calculates the costs.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        public static void CalculateCosts(this Statistic statistic, IReadOnlyCollection<ITransaction> transactions)
        {
            if (transactions.Count == 0)
                return;

            statistic.SumOrderCosts = transactions.Sum(t => t.OrderCosts);
            statistic.SumTaxes = transactions.OfType<ISellingTransaction>().Sum(t => t.Taxes) +
                                 transactions.OfType<IDividendTransaction>().Sum(t => t.Taxes);
            statistic.AverageBuyVolume = decimal.Round(transactions.Average(t => t.PricePerShare * t.Shares), 2);
        }
    }
}
