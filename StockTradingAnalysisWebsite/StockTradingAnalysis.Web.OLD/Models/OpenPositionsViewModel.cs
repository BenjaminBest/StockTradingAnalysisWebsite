using System.Collections.Generic;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The v contains all information about open positions, as well as risk and capital expenditure
    /// </summary>
    public class OpenPositionsViewModel
    {
        /// <summary>
        /// Gets or sets the open positions.
        /// </summary>
        /// <value>
        /// The open positions.
        /// </value>
        public IList<OpenPositionViewModel> OpenPositions { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public OpenPositionSummaryViewModel Summary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenPositionsViewModel"/> class.
        /// </summary>
        public OpenPositionsViewModel()
        {
            OpenPositions = new List<OpenPositionViewModel>();
        }
    }
}