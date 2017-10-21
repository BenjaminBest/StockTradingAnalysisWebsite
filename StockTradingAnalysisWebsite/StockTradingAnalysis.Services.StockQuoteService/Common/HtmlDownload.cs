using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockTradingAnalysis.Services.StockQuoteService.Common
{
    /// <summary>
    /// HtmlDownload is used to download synchronous and asynchronous a web page
    /// </summary>
    public static class HtmlDownload
    {
        /// <summary>
        /// Downloads the given <param name="uri">Url</param> synchronous with HttpClient
        /// </summary>
        /// <param name="uri">Url</param>
        /// <returns></returns>
        public static string CreateHttpClientSync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    return responseContent.ReadAsStringAsync().Result;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Downloads the given <param name="uri">Url</param> asynchronous with HttpClient
        /// </summary>
        /// <param name="uri">Url</param>
        /// <returns></returns>
        public static async Task<string> CreateHttpClient(Uri uri)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(uri);
            }
        }
    }
}