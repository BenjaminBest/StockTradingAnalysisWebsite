using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using StockTradingAnalysis.Services.StockQuoteService.Common;

namespace StockTradingAnalysis.Services.StockQuoteService.Providers
{
    /// <summary>
    /// Class QuotationDownloadBoerseDuesseldorf is a class to handle the response of a boerse duesseldorf stock prices web page
    /// </summary>
    public class QuotationDownloadBoerseDuesseldorf : QuotationDownload
    {
        /// <summary>
        /// Extract the needed information out of the plain html response. This si different for multiple pages.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Quotation> ExtractInformation()
        {
            var quotations = new List<Quotation>();

            var document = new HtmlDocument();

            if (string.IsNullOrEmpty(Response))
            {
                Status.HttpResponseCode = 404;
                return Enumerable.Empty<Quotation>();
            }

            document.LoadHtml(Response);

            var isinRaw = document.DocumentNode
                .Descendants("div")
                .FirstOrDefault(d => d.GetAttributeValue("class", "") == "wkn_isin");

            if (isinRaw == null)
            {
                Status.HttpResponseCode = 404;
                return Enumerable.Empty<Quotation>();
            }

            var isin = isinRaw.InnerText.Split(',')[1].Split(':')[1].Trim();
            var nodes = document.DocumentNode.Descendants("tr");

            foreach (var node in nodes)
            {
                //.ChildNodes[0].Attributes["class"].Value != "right first num"
                if (node.ChildNodes.Count() != 6 || node.ParentNode.Name != "tbody")
                    continue;

                decimal open;
                decimal low;
                decimal high;
                decimal close;
                DateTime date;

                var validQuote = true;

                var formatting = new NumberFormatInfo
                {
                    CurrencyDecimalSeparator = ","
                };

                if (!DateTime.TryParseExact(node.ChildNodes[0].InnerText, "dd'.'MM'.'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    validQuote = false;
                }
                if (!decimal.TryParse(node.ChildNodes[1].ChildNodes[0].InnerText, NumberStyles.Currency, formatting, out open))
                {
                    validQuote = false;
                }
                if (!decimal.TryParse(node.ChildNodes[2].ChildNodes[0].InnerText, NumberStyles.Currency, formatting, out high))
                {
                    validQuote = false;
                }
                if (!decimal.TryParse(node.ChildNodes[3].ChildNodes[0].InnerText, NumberStyles.Currency, formatting, out low))
                {
                    validQuote = false;
                }
                if (!decimal.TryParse(node.ChildNodes[4].ChildNodes[0].InnerText, NumberStyles.Currency, formatting, out close))
                {
                    validQuote = false;
                }
                //Volume

                if (!validQuote)
                    continue;

                var quote = new Quotation(Wkn, isin)
                {
                    Date = date,
                    Open = open,
                    Low = low,
                    High = high,
                    Close = close
                };

                quotations.Add(quote);
            }

            return quotations;
        }
    }
}