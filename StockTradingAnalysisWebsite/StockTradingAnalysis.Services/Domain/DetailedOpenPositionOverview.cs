using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The DetailedOpenPositionOverview contains all open positions and additional information.
    /// </summary>
    /// <seealso cref="IDetailedOpenPositionOverview" />
    public class DetailedOpenPositionOverview : IDetailedOpenPositionOverview
    {
        /// <summary>
        /// Gets the open positions.
        /// </summary>
        /// <value>
        /// The open positions.
        /// </value>
        public IEnumerable<IDetailedOpenPosition> OpenPositions { get; set; }

        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public IDetailedOpenPositionSummary Summary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedOpenPositionOverview"/> class.
        /// </summary>
        public DetailedOpenPositionOverview()
        {
            OpenPositions = new List<IDetailedOpenPosition>();
        }
    }
}
