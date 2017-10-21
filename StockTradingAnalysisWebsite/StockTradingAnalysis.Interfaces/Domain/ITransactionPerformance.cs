using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Interfaces.Types;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines an interface for performance calculation results of a selling/dividend statistic
    /// </summary>
    public interface ITransactionPerformance : IModelRepositoryItem
    {
        /// <summary>
        /// Gets the absolute profit
        /// </summary>
        decimal ProfitAbsolute { get; }

        /// <summary>
        /// Gets the profit (in %)
        /// </summary>
        decimal ProfitPercentage { get; }

        /// <summary>
        /// Gets <c>true</c> if profit was made
        /// </summary>
        bool ProfitMade { get; }

        /// <summary>
        /// Gets the holding period
        /// </summary>
        HoldingPeriod HoldingPeriod { get; }

        /// <summary>
        /// Gets the maximum risk per trade
        /// </summary>
        decimal R { get; }

        /// <summary>
        /// Gets the exit efficiency (based on MAE,MFE)
        /// </summary>
        decimal? ExitEfficiency { get; }

        /// <summary>
        /// Gets the entry efficiency (based on MAE,MFE)
        /// </summary>
        decimal? EntryEfficiency { get; }

        /// <summary>
        /// Gets the maximum loss during trade incl. order costs
        /// </summary>
        decimal? MAEAbsolute { get; }

        /// <summary>
        /// Gets the maximum profit during trade incl. order costs
        /// </summary>
        decimal? MFEAbsolute { get; }
    }
}