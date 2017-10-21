using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for a strategy
    /// </summary>
    public interface IStrategy : IVersionableModelRepositoryItem
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
        /// Gets the image
        /// </summary>
        IImage Image { get; }
    }
}