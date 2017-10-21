using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IFeedbackProportion describes the proportion of one type of feedback in relation to all feedbacks 
    /// </summary>
    public interface IFeedbackProportion : IModelRepositoryItem
    {
        /// <summary>
        /// Gets the amount of this type of feedbacks overall
        /// </summary>
        decimal FeedbackShare { get; set; }

        /// <summary>
        /// Gets the amount of all feedbacks
        /// </summary>
        decimal OverallFeedbackAmount { get; set; }

        /// <summary>
        /// Gets the proportion calculation by using <see cref="OverallFeedbackAmount"/> and <see cref="FeedbackShare"/>
        /// </summary>
        decimal ProportionPercentage { get; }
    }
}