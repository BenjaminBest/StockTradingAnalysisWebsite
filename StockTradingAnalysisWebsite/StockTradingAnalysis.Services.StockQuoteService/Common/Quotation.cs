using System;

namespace StockTradingAnalysis.Services.StockQuoteService.Common
{
    /// <summary>
    /// Quotation of a share
    /// </summary>
    public class Quotation
    {
        /// <summary>
        /// Gets/sets the ISIN
        /// </summary>
        public string Isin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the WKN
        /// </summary>
        public string Wkn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the date
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the high value
        /// </summary>
        public decimal High
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the close value
        /// </summary>
        public decimal Close
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the open value
        /// </summary>
        public decimal Open
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the low value
        /// </summary>
        public decimal Low
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/sets the changed date
        /// </summary>
        public DateTime Changed
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes this quotation with a changed date
        /// </summary>
        /// <param name="wkn">WKN</param>
        /// <param name="isin">ISIN</param>
        public Quotation(string wkn, string isin)
        {
            Changed = DateTime.Now;

            Wkn = wkn;
            Isin = isin;
        }
    }
}
