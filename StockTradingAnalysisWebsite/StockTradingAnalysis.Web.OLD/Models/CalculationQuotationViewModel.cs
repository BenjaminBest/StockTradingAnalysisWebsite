using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// Class ViewModelCalculationQuotation is used for one single quotation
    /// </summary>
    public class CalculationQuotationViewModel
    {
        /// <summary>
        /// Gets/sets the name
        /// </summary>
        [Display(Name = "Display_Quotation", ResourceType = typeof (Resources))]
        [DisplayFormat(NullDisplayText = "")]
        public decimal Quotation { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        [Display(Name = "Display_QuotationUnderlying", ResourceType = typeof (Resources))]
        public decimal QuotationUnderlying { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        [UIHint("PerformanceAbsolute")]
        [Display(Name = "Display_QuotationPLAbsolute", ResourceType = typeof (Resources))]
        public decimal PlAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        [UIHint("PerformancePercentage")]
        [Display(Name = "Display_QuotationPLPercentage", ResourceType = typeof (Resources))]
        public decimal PlPercentage { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at break even
        /// </summary>
        public bool IsBreakEven { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the stopp loss
        /// </summary>
        public bool IsStoppLoss { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the take profit
        /// </summary>
        public bool IsTakeProfit { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if this is the current quotation
        /// </summary>
        public bool IsCurrentQuotation { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the buying quotation
        /// </summary>
        public bool IsBuy { get; set; }
    }
}