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
        /// <param name="feedback">The feedback.</param>
        /// <param name="proportions">The feedback proportion in relation of the total amount of feedback given.</param>
        public static void CalculateFeedback(this Statistic statistic,
            IReadOnlyCollection<IFeedback> feedback,
            IReadOnlyCollection<IFeedbackProportion> proportions)
        {
            statistic.FeedbackTop5 = proportions.Join(feedback, p => p.Id, f => f.Id,
                    (p, f) => new { f.Name, p.ProportionPercentage })
                .OrderByDescending(p => p.ProportionPercentage).Take(5)
                .ToDictionary(k => k.Name, v => v.ProportionPercentage);
        }
    }
}
