using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.External.Common;

namespace StockTradingAnalysis.Services.External.Providers
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
        public override IEnumerable<IQuotation> ExtractInformation()
        {
            var quotations = new List<Quotation>();

            var document = new HtmlDocument();

            if (string.IsNullOrEmpty(Response))
                return Enumerable.Empty<Quotation>();

            document.LoadHtml(Response);

            var isinRaw = document.DocumentNode
                .Descendants("div")
                .FirstOrDefault(d => d.GetAttributeValue("class", "") == "wkn_isin");

            if (isinRaw == null)
                return Enumerable.Empty<Quotation>();

            var isin = isinRaw.InnerText.Split(',')[1].Split(':')[1].Trim();
            var nodes = document.DocumentNode.Descendants("tr");

            foreach (var node in nodes)
            {
                //.ChildNodes[0].Attributes["class"].Value != "right first num"
                if (node.ChildNodes.Count() != 6 || node.ParentNode.Name != "tbody")
                    continue;

                var validQuote = true;

                var formatting = new NumberFormatInfo
                {
                    CurrencyDecimalSeparator = ","
                };

                if (!DateTime.TryParseExact(node.ChildNodes[0].InnerText, "dd'.'MM'.'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    validQuote = false;
                }
                if (!decimal.TryParse(node.ChildNodes[1].ChildNodes[0].InnerText, NumberStyles.Currency, formatting, out var open))
                {
                    validQuote = false;
                }
                if (!decimal.TryParse(node.ChildNodes[2].ChildNodes[0].InnerText, NumberStyles.Currency, formatting, out var high))
                {
                    validQuote = false;
                }
                if (!decimal.TryParse(node.ChildNodes[3].ChildNodes[0].InnerText, NumberStyles.Currency, formatting, out var low))
                {
                    validQuote = false;
                }
                if (!decimal.TryParse(node.ChildNodes[4].ChildNodes[0].InnerText, NumberStyles.Currency, formatting, out var close))
                {
                    validQuote = false;
                }
                //Volume

                if (!validQuote)
                    continue;

                var quote = new Quotation()
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