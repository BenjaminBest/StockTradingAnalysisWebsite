using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The v contains all information about open positions, as well as risk and capital expenditure
    /// </summary>
    public class OpenPositionsViewModel
    {
        /// <summary>
        /// Current amount of capital in market
        /// </summary>
        [Display(Name = "Display_ViewModelOpenTransactionsCapitalExpenditure", ResourceType = typeof(Resources))]
        [UIHint("PerformanceRedAbsolute")]
        public decimal CapitalExpenditure { get; set; }

        /// <summary>
        /// Gets or sets the open positions.
        /// </summary>
        /// <value>
        /// The open positions.
        /// </value>
        public IList<OpenPositionViewModel> OpenPositions { get; set; }
    }
}