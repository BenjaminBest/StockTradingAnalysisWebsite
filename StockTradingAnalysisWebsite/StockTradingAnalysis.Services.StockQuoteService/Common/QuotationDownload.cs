using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace StockTradingAnalysis.Services.StockQuoteService.Common
{
    /// <summary>
    /// Class QuotationDownload is a base class which is able to download a page. The data handling logic must
    /// be implmented in the derived class.
    /// </summary>
    public abstract class QuotationDownload
    {
        /// <summary>
        /// WKN
        /// </summary>
        protected string Wkn;

        /// <summary>
        /// Url to download
        /// </summary>
        private Uri _website;

        /// <summary>
        /// Response data
        /// </summary>
        protected string Response;

        /// <summary>
        /// Status of the request
        /// </summary>
        public Status Status;

        /// <summary>
        /// Initializes this class with default values
        /// </summary>
        protected QuotationDownload()
        {
            Status = new Status();
        }

        /// <summary>
        /// Initializes this class
        /// </summary>
        /// <param name="wkn">WKN</param>
        /// <param name="website">Url</param>
        /// <returns></returns>
        public QuotationDownload Initialize(string wkn, string website)
        {
            Wkn = wkn;
            _website = new Uri(website);

            return this;
        }

        /// <summary>
        /// Downloads the url and writes the result response
        /// </summary>
        /// <returns></returns>
        public QuotationDownload Download()
        {
            try
            {
                Response = HtmlDownload.CreateHttpClient(_website).Result;
            }
            catch (HttpRequestException)
            {
                Status.HttpResponseCode = 504;
            }
            catch(Exception)
            {
                Status.HttpResponseCode = 500;
            }

            return this;
        }

        /// <summary>
        /// Extract the needed information out of the plain html response. This si different for multiple pages.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<Quotation> ExtractInformation();
    }
}