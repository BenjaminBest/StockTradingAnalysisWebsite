using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Modules
{
    /// <summary>
    /// The StatisticFeedbackModule is a calculation module for the statistic service to calculate feedback related information.
    /// </summary>
    public static class StatisticFeedbackModule
    {
        /// <summary>
        /// Calculates the feedback.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <param name="transactions">The transactions.</param>
        public static void CalculateFeedback(this Statistic statistic, IReadOnlyList<ITransaction> transactions)
        {
            var allFeedback = transactions.OfType<ISellingTransaction>().SelectMany(t => t.Feedback)
                .Select(t => t.Name).ToList();

            statistic.FeedbackTop5 = allFeedback
                .GroupBy(t => t)
                .Select(x => new { Name = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToDictionary(g => g.Name, g => decimal.Round(g.Count / allFeedback.Count() * 100, 2));
        }
    }
}
