using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class TransactionSellingViewModel : TransactionViewModel
    {
        /// <summary>
        /// Gets/sets the taxes paid
        /// </summary>
        [Required(ErrorMessageResourceName = "Validation_TransactionsTaxesRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_TransactionsTaxes", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "0,00 €")]
        [UIHint("Currency")]
        public decimal Taxes { get; set; }

        /// <summary>
        /// Gets/sets the minimum quote during trade
        /// </summary>
        [Display(Name = "Display_TransactionsMAE", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "0,00 €")]
        [UIHint("Currency")]
        public decimal? MAE { get; set; }

        /// <summary>
        /// Gets/sets the maximum quote during trade
        /// </summary>
        [Display(Name = "Display_TransactionsMFE", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "0,00 €")]
        [UIHint("Currency")]
        public decimal? MFE { get; set; }

        /// <summary>
        /// Gets/sets the feedbacks
        /// </summary>
        [Display(Name = "Display_TransactionsFeedback", ResourceType = typeof(Resources))]
        public IEnumerable<FeedbackViewModel> Feedback { get; set; }
    }
}