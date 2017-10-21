using System;

namespace StockTradingAnalysis.Web.Models
{
    public class FeedbackProportionViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// The proportion of this feedback on all transactions
        /// </summary>
        public decimal Proportion { get; set; }
    }
}