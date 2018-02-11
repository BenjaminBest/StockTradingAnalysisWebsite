using System;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// Viewmodel for the transaction history of one stock
    /// </summary>
    public class TransactionHistoryViewModel
    {
        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        [DataType(DataType.DateTime)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        [Display(Name = "Display_TransactionsOrderDate", ResourceType = typeof(Resources))]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets a transaction description if this was a buy, sell or dividend
        /// </summary>        
        [Display(Name = "Display_TransactionsAction", ResourceType = typeof(Resources))]
        [UIHint("Action")]
        public string Action { get; set; }

        /// <summary>
        /// Gets/sets the amount of units
        /// </summary>
        [Display(Name = "Display_TransactionsUnits", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Units { get; set; }

        /// <summary>
        /// Gets/sets the price per unit
        /// </summary>
        [Display(Name = "Display_TransactionsPricePerUnit", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false)]
        public decimal PricePerUnit { get; set; }

        /// <summary>
        /// Gets the position size (amount of money put in the trade, without transaction costs)
        /// </summary>
        [Display(Name = "Display_TransactionsPositionSize", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €")]
        public decimal PositionSize { get; set; }
    }
}