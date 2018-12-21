using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class TransactionDividendViewModel : TransactionViewModel
    {
        /// <summary>
        /// Gets/sets the taxes paid
        /// </summary>
        [Required(ErrorMessageResourceName = "Validation_TransactionsTaxesRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_TransactionsTaxes", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "0,00 €")]
        public decimal Taxes { get; set; }
    }
}