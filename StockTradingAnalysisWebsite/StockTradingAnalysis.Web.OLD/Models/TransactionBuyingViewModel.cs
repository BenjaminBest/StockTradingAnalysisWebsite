using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class TransactionBuyingViewModel : TransactionViewModel
    {
        /// <summary>
        /// Gets/sets the initial stop loss
        /// </summary>
        [Display(Name = "Display_TransactionsInitialSL", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "0,00 €")]
        public decimal InitialSL { get; set; }

        /// <summary>
        /// Gets/sets the initial take profit
        /// </summary>
        [Display(Name = "Display_TransactionsInitialTP", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "0,00 €")]
        public decimal InitialTP { get; set; }

        /// <summary>
        /// Gets/sets the strategy used
        /// </summary>
        [Display(Name = "Display_TransactionsStrategy", ResourceType = typeof(Resources))]
        public StrategyViewModel Strategy { get; set; }
    }
}