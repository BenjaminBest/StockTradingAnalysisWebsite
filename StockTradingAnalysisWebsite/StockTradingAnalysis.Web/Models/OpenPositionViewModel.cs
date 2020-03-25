using System;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The OpenPositionViewModel contains bought shares, average buying price etc. for one open position.
    /// </summary>
    public class OpenPositionViewModel
    {
        /// <summary>
        /// Gets or sets the stock identifier.
        /// </summary>
        /// <value>
        /// The stock identifier.
        /// </value>        
        public Guid StockId { get; set; }

        /// <summary>
        /// Gets or sets the name of the stock.
        /// </summary>
        /// <value>
        /// The name of the stock.
        /// </value>
        [Display(Name = "Display_ViewModelOpenTransactionStocksName", ResourceType = typeof(Resources))]
        public string StockName { get; set; }

        /// <summary>
        /// Gets or sets the first order date.
        /// </summary>
        /// <value>
        /// The first order date.
        /// </value>
        [Display(Name = "Display_ViewModelOpenTransactionMinOrderDate", ResourceType = typeof(Resources))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime FirstOrderDate { get; set; }

        /// <summary>
        /// Gets or sets the size of the position.
        /// </summary>
        /// <value>
        /// The size of the position.
        /// </value>
        [Display(Name = "Display_ViewModelOpenTransactionPositionSize", ResourceType = typeof(Resources))]
        [UIHint("Currency")]
        public decimal PositionSize { get; set; }

        /// <summary>
        /// Gets or sets the shares.
        /// </summary>
        /// <value>
        /// The shares.
        /// </value>
        [Display(Name = "Display_ViewModelOpenTransactionUnits", ResourceType = typeof(Resources))]
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets or sets the average price per share.
        /// </summary>
        /// <value>
        /// The average price per share.
        /// </value>
        [Display(Name = "Display_ViewModelOpenTransactionPricePerUnit", ResourceType = typeof(Resources))]
        [UIHint("Currency")]
        public decimal AveragePricePerShare { get; set; }

        /// <summary>
        /// Gets or sets the current quotation.
        /// </summary>
        /// <value>
        /// The current quotation.
        /// </value>
        [Display(Name = "Display_ViewModelOpenPositionCurrentQuotation", ResourceType = typeof(Resources))]
        [UIHint("Quotation")]
        public QuoteViewModel CurrentQuotation { get; set; }

        /// <summary>
        /// Gets or sets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        [Display(Name = "Display_ViewModelOpenPositionProfit", ResourceType = typeof(Resources))]
        [UIHint("Profit")]
        public ProfitViewModel Profit { get; set; }

        /// <summary>
        /// Gets or sets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        [Display(Name = "Display_ViewModelOpenPositionYTDProfit", ResourceType = typeof(Resources))]
        [UIHint("Profit")]
        public ProfitViewModel YearToDateProfit { get; set; }
    }
}