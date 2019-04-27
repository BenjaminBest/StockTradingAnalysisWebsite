using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class TransactionIndexViewModel : TransactionViewModel
    {
        /// <summary>
        /// Gets the position size (amount of money put in the trade, without transaction costs)
        /// </summary>
        [Display(Name = "Display_TransactionsPositionSize", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false)]
        public decimal PositionSize { get; set; }

        [UIHint("PerformanceAbsolute")]
        [Display(Name = "Display_StatisticsTotalCumulatedPL", ResourceType = typeof(Resources))]
        public decimal? AccountBalance { get; set; }
    }
}