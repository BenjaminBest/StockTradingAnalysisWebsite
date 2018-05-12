using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IDetailedOpenPositionOverview defines all open positions and information for all positions.
    /// </summary>
    public interface IDetailedOpenPositionOverview
    {
        /// <summary>
        /// Gets the capital expenditure / the amount of capital in the market.
        /// </summary>
        /// <value>
        /// The capital expenditure.
        /// </value>
        decimal CapitalExpenditure { get; }

        /// <summary>
        /// Gets the open positions.
        /// </summary>
        /// <value>
        /// The open positions.
        /// </value>
        IEnumerable<IDetailedOpenPosition> OpenPositions { get; }
    }
}
