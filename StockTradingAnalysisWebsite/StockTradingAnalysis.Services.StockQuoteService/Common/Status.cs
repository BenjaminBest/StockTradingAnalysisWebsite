using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTradingAnalysis.Services.StockQuoteService.Common
{
    /// <summary>
    /// Class Status contains a http status and additional information
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Gets/sets the status response code
        /// </summary>
        public int HttpResponseCode { get; set; }

        /// <summary>
        /// Initializes the status with OK (200)
        /// </summary>
        public Status()
        {
            HttpResponseCode = 200;
        }
    }
}
