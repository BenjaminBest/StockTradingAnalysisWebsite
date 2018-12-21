using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The ProfitViewModel defines information about the transaction result.
    /// </summary>
    public class ProfitViewModel
    {

        /// <summary>
        /// Gets or sets the absolute profit.
        /// </summary>
        /// <value>
        /// The absolute profit.
        /// </value>
        [Display(Name = "Display_ViewModelOpenPositionAbsoluteProfit", ResourceType = typeof(Resources))]
        [UIHint("PerformanceAbsolute")]
        public decimal AbsoluteProfit { get; set; }

        /// <summary>
        /// Gets or sets the percentage profit.
        /// </summary>
        /// <value>
        /// The percentage profit.
        /// </value>
        [Display(Name = "Display_ViewModelOpenPositionPercentageProfit", ResourceType = typeof(Resources))]
        [UIHint("PerformancePercentage")]
        public decimal PercentageProfit { get; set; }
    }
}