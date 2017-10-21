using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for a selling (closing) transaction
    /// </summary>
    public interface ISellingTransaction : ITransaction
    {
        /// <summary>
        /// Gets the taxes paid
        /// </summary>
        decimal Taxes { get; }

        /// <summary>
        /// Gets the minimum quote during trade
        /// </summary>
        decimal? MAE { get; }

        /// <summary>
        /// Gets the maximum quote during trade
        /// </summary>
        decimal? MFE { get; }

        /// <summary>
        /// Gets the feedbacks
        /// </summary>
        IEnumerable<IFeedback> Feedback { get; }
    }
}