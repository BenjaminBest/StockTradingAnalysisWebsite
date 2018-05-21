using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IDetailedOpenPositionOverview defines all open positions and information for all positions.
    /// </summary>
    public interface IDetailedOpenPositionOverview
    {
        /// <summary>
        /// Gets the open positions.
        /// </summary>
        /// <value>
        /// The open positions.
        /// </value>
        IEnumerable<IDetailedOpenPosition> OpenPositions { get; }

        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        IDetailedOpenPositionSummary Summary { get; }
    }
}
