using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The OpenPositionSummaryViewModel contains a summary of the open positions with cumulated values like overall profit.
    /// </summary>
    public class OpenPositionSummaryViewModel
    {
        /// <summary>
        /// Gets or sets the size of the position.
        /// </summary>
        /// <value>
        /// The size of the position.
        /// </value>
        [Display(Name = "Display_ViewModelOpenTransactionPositionSize", ResourceType = typeof(Resources))]
        [UIHint("PerformanceRedAbsolute")]
        public decimal PositionSize { get; set; }

        /// <summary>
        /// Gets or sets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        [Display(Name = "Display_ViewModelOpenPositionProfit", ResourceType = typeof(Resources))]
        [UIHint("Profit")]
        public ProfitViewModel Profit { get; set; }
    }
}