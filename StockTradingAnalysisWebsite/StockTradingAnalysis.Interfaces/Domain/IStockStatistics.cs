using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IAccountBalance describes a single balance for an account
    /// </summary>
    public interface IStockStatistics : IModelRepositoryItem
    {
        /// <summary>
        /// Gets the performance
        /// </summary>
        decimal Performance { get; }
    }
}