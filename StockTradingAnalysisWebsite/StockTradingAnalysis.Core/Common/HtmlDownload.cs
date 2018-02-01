using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockTradingAnalysis.Core.Common
{
    /// <summary>
    /// HtmlDownload is used to download synchronous and asynchronous a web page
    /// </summary>
    public static class HtmlDownload
    {
        /// <summary>
        /// Downloads the given <param name="uri">Url</param> asynchronous with WebRequest
        /// </summary>
        /// <param name="uri">Url</param>
        /// <returns></returns>
        public static async Task<string> CreateWebRequest(Uri uri)
        {
            var request = WebRequest.Create(uri);
            HttpWebResponse response = null;

            response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse,
                request.EndGetResponse, null);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Downloads the given <param name="uri">Url</param> synchronous with HttpClient and using a timeout of <paramref name="timeout" />
        /// </summary>
        /// <param name="uri">Url</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static string CreateHttpClientSync(Uri uri, TimeSpan timeout)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = timeout;
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