using System.Collections.Generic;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The class UpdateQuotationsViewModel contains the information needed to update the quotations for all stocks
    /// </summary>
    public class UpdateQuotationsViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the quotation service is available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the service is available; otherwise, <c>false</c>.
        /// </value>
        public bool IsServiceAvailable { get; set; }

        /// <summary>
        /// Gets or sets the stocks.
        /// </summary>
        /// <value>
        /// The stocks.
        /// </value>
        public IEnumerable<StockViewModel> Stocks { get; set; }
    }
}