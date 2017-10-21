using System;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }

        public int OriginalVersion { get; set; }

        /// <summary>
        /// Gets/sets the order date
        /// </summary>
        [Required(ErrorMessageResourceName = "Validation_TransactionsOrderDateRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        [Display(Name = "Display_TransactionsOrderDate", ResourceType = typeof(Resources))]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets/sets the amount of units
        /// </summary>
        [Required(ErrorMessageResourceName = "Validation_TransactionsUnitsRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_TransactionsUnits", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Units { get; set; }

        /// <summary>
        /// Gets/sets the price per unit
        /// </summary>
        [Required(ErrorMessageResourceName = "Validation_TransactionsPricePerUnitRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_TransactionsPricePerUnit", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false)]
        public decimal PricePerUnit { get; set; }

        /// <summary>
        /// Gets/sets the order costs
        /// </summary>
        [Required(ErrorMessageResourceName = "Validation_TransactionsOrderCostsRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_TransactionsOrderCosts", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false)]
        public decimal OrderCosts { get; set; }

        /// <summary>
        /// Gets/sets the description
        /// </summary>
        [Display(Name = "Display_TransactionsDescription", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "-")]
        public string Description { get; set; }

        /// <summary>
        /// Gets/sets the tag
        /// </summary>
        [Display(Name = "Display_TransactionsTag", ResourceType = typeof(Resources))]
        [DataType(DataType.Text)]
        [DisplayFormat(NullDisplayText = "-")]
        public string Tag { get; set; }

        [UIHint("ViewModelImage")]
        public ImageViewModel Image { get; set; }

        /// <summary>
        /// Gets/sets the underlying stock
        /// </summary>
        [Display(Name = "Display_TransactionsStock", ResourceType = typeof(Resources))]
        public StockViewModel Stock { get; set; }

        /// <summary>
        /// Gets a transaction description if this was a buy, sell or dividend
        /// </summary>        
        [Display(Name = "Display_TransactionsAction", ResourceType = typeof(Resources))]
        public string Action { get; set; }
    }
}