using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using StockTradingAnalysis.Services.StockQuoteService.Common;

namespace StockTradingAnalysis.Services.StockQuoteService.Providers
{
    /// <summary>
    /// Class QuotationDownloadOnVista is a class to handle the response of a onvista stock prices web page
    /// </summary>
    public class QuotationDownloadOnVista : QuotationDownload
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
                return null;

            document.LoadHtml(Response);

            if (document.DocumentNode
                    .Descendants("td")
                    .Where(d => d.GetAttributeValue("class", "") == "s1r")
                    .FirstOrDefault(d => d.InnerText == "Zu Ihrer Eingabe wurden keine Daten gefunden.") != null)
                return Enumerable.Empty<Quotation>();

            var isin = document.DocumentNode
                .Descendants("td")
                .Where(d => d.GetAttributeValue("class", "") == "skb")
                .FirstOrDefault(d => d.InnerText == "ISIN:").NextSibling.InnerText;

            var nodes = document.DocumentNode.Descendants("tr")
                .Where(d=>d.GetAttributeValue("class","") == "hr");
            foreach (var node in nodes)
            {
                if (node.ChildNodes.Count() != 5)
                    continue;                

                decimal open;
                decimal low;
                decimal high;
                decimal close;
                DateTime date;

                if (!DateTime.TryParse(node.ChildNodes[0].InnerText, out date))
                {
                    break;
                }
                if (!decimal.TryParse(node.ChildNodes[1].InnerText, out open))
                {
                    break;
                }
                if (!decimal.TryParse(node.ChildNodes[2].InnerText, out low))
                {
                    break;
                }
                if (!decimal.TryParse(node.ChildNodes[3].InnerText, out high))
                {
                    break;
                }
                if (!decimal.TryParse(node.ChildNodes[4].InnerText, out close))
                {
                    break;
                }

                var quote = new Quotation(Wkn, isin )
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