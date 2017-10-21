using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for a feedback
    /// </summary>
    public interface IFeedback : IVersionableModelRepositoryItem
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the current overall performance for this stock
        /// </summary>
        decimal Performance { get; }
    }
}