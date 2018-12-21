using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// Viewmodel for a stock
    /// </summary>
    public class StockViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the original version.
        /// </summary>
        /// <value>
        /// The original version.
        /// </value>
        public int OriginalVersion { get; set; }

        /// <summary>
        /// Gets or sets the stocks description.
        /// </summary>
        /// <value>
        /// The stocks description.
        /// </value>
        public string StocksDescription { get; set; }

        /// <summary>
        /// Gets or sets the stocks short description.
        /// </summary>
        /// <value>
        /// The stocks short description.
        /// </value>
        public string StocksShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessageResourceName = "Validation_StockNameRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_StockName", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the WKN.
        /// </summary>
        /// <value>
        /// The WKN.
        /// </value>
        [Required(ErrorMessageResourceName = "Validation_StockWKNRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_StockWKN", ResourceType = typeof(Resources))]
        public string Wkn { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [Required(ErrorMessageResourceName = "Validation_StockTypeIDRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_StockType", ResourceType = typeof(Resources))]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the long short.
        /// </summary>
        /// <value>
        /// The long short.
        /// </value>
        [Required(ErrorMessageResourceName = "Validation_StockLongShortRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_LongShort", ResourceType = typeof(Resources))]
        [UIHint("LongShort")]
        public string LongShort { get; set; }

        /// <summary>
        /// Gets or sets the performance.
        /// </summary>
        /// <value>
        /// The performance.
        /// </value>
        [UIHint("PerformanceAbsolute")]
        public decimal Performance { get; set; }

        /// <summary>
        /// Gets or sets the transaction history.
        /// </summary>
        /// <value>
        /// The transaction history.
        /// </value>
        [Display(Name = "Display_StockTransactionHistory", ResourceType = typeof(Resources))]
        public IList<TransactionHistoryViewModel> TransactionHistory { get; set; }

        /// <summary>
        /// Gets or sets the lastest quote.
        /// </summary>
        /// <value>
        /// The lastest quote.
        /// </value>
        [Display(Name = "Display_StockLastestQuote", ResourceType = typeof(Resources))]
        public QuoteViewModel LastestQuote { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockViewModel"/> class.
        /// </summary>
        public StockViewModel()
        {
            TransactionHistory = new List<TransactionHistoryViewModel>();
        }
    }
}