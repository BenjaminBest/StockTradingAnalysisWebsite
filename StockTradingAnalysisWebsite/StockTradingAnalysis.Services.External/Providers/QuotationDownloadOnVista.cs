using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.External.Common;

namespace StockTradingAnalysis.Services.External.Providers
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
        public override IEnumerable<IQuotation> ExtractInformation()
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

            var nodes = document.DocumentNode.Descendants("tr")
                .Where(d => d.GetAttributeValue("class", "") == "hr");
            foreach (var node in nodes)
            {
                if (node.ChildNodes.Count() != 5)
                    continue;

                if (!DateTime.TryParse(node.ChildNodes[0].InnerText, out var date))
                {
                    break;
                }
                if (!decimal.TryParse(node.ChildNodes[1].InnerText, out var open))
                {
                    break;
                }
                if (!decimal.TryParse(node.ChildNodes[2].InnerText, out var low))
                {
                    break;
                }
                if (!decimal.TryParse(node.ChildNodes[3].InnerText, out var high))
                {
                    break;
                }
                if (!decimal.TryParse(node.ChildNodes[4].InnerText, out var close))
                {
                    break;
                }

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