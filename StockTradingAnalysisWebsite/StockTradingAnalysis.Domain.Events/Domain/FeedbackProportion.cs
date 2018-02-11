using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    public class FeedbackProportion : IFeedbackProportion
    {
        /// <summary>
        /// Gets the id of a feedback
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the amount of this type of feedbacks overall
        /// </summary>
        public decimal FeedbackShare { get; set; }

        /// <summary>
        /// Gets the amount of all feedbacks
        /// </summary>
        public decimal OverallFeedbackAmount { get; set; }

        /// <summary>
        /// Gets the proportion calculation by using <see cref="IFeedbackProportion.OverallFeedbackAmount"/> and <see cref="IFeedbackProportion.FeedbackShare"/>
        /// </summary>
        public decimal ProportionPercentage => OverallFeedbackAmount == 0 ? 0 : decimal.Round(FeedbackShare / OverallFeedbackAmount * 100, 2);

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id"></param>
        public FeedbackProportion(Guid id)
        {
            Id = id;
        }
    }
}
