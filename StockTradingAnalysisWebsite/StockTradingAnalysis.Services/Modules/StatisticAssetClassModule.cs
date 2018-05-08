using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Extensions;
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
        /// Calculates asset based statistics.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactionPerformances">The transaction performances.</param>
        /// <param name="transactions">The transactions.</param>
        public static void CalculateAssetClass(this Statistic statistic,
            IReadOnlyList<ITransactionPerformance> transactionPerformances,
            IReadOnlyList<ITransaction> transactions)
        {
            var stockStatistics = from t in transactions.OfTypes<ITransaction, ISellingTransaction, IDividendTransaction>()
                                  join tp in transactionPerformances on t.Id equals tp.Id
                                  select new { Performance = tp, StockType = t.Stock.Type, StockName = t.Stock.Name };

            var statistics = stockStatistics.ToList();

            var perType = from st in statistics
                          group st by st.StockType into g
                          select new { Type = g.Key, Profit = g.Sum(p => p.Performance.ProfitAbsolute) };

            var perStock = from st in statistics
                           group st by st.StockName into g
                           select new { Name = g.Key, Profit = g.Sum(p => p.Performance.ProfitAbsolute) };


            statistic.AbsoluteProfitPerStockName = perStock.ToDictionary(pr => pr.Name, pr => pr.Profit);
            statistic.AbsoluteProfitPerTradingType = perType.ToDictionary(pr => pr.Type, pr => pr.Profit);

            var bestAssetClass =
                statistic.AbsoluteProfitPerTradingType.OrderByDescending(t => t.Value).FirstOrDefault();

            statistic.BestAssetClassProfit = bestAssetClass.Value;
            statistic.BestAssetClassName = bestAssetClass.Key;

            var bestAsset =
                statistic.AbsoluteProfitPerStockName.OrderByDescending(t => t.Value).FirstOrDefault();


            statistic.BestAssetProfit = bestAsset.Value;
            statistic.BestAssetName = bestAsset.Key;
        }
    }
}
