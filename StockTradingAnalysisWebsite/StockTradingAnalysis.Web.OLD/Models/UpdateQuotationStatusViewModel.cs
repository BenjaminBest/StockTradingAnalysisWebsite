using System;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// Class UpdateQuotationStatusViewModel contains information about the update/retrival of share prices for a financial product
    /// </summary>
    public class UpdateQuotationStatusViewModel
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        public UpdateQuotationStatusViewModel()
        {
            TimeStamp = DateTime.Now;
        }

        /// <summary>
        /// Gets/sets the id of this stock
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets if no error occured
        /// </summary>
        public bool Successfull
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the message with status or error information
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets/sets date and time when update occured
        /// </summary>
        public DateTime TimeStamp
        {
            get;
            set;
        }
    }
}