using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Web.Migration.Common;

namespace StockTradingAnalysis.Web.Migration.Importer
{
    public class DownloadQuotations : ImportBase
    {
        public void Start()
        {

            //Import            
            LoggingService.Info("Download Quotations ");

            var stocks = QueryDispatcher.Execute(new StockAllQuery());

            foreach (var stock in stocks)
            {
                var quotationsBefore = QueryDispatcher.Execute(new StockQuotationsCountByIdQuery(stock.Id));

                var result = string.Empty;
                try
                {
                    result = HtmlDownload.CreateHttpClientSync(new Uri(Configuration.GetValue<string>("StockQuoteServiceBaseUrl") + $"/{stock.Wkn}"));
                }
                catch (Exception e)
                {
                    // ignored
                }

                if (string.IsNullOrEmpty(result))
                {
                    LoggingService.Info($"No quotations for stock {stock.Name} imported (Qty Before: {quotationsBefore})");
                    continue;
                }

                var quotations = SerializerService.Deserialize<IEnumerable<Quotation>>(result).ToList();

                if (quotations.Any())
                {
                    var cmd = new StockQuotationsAddOrChangeCommand(
                        stock.Id,
                        stock.OriginalVersion,
                        quotations);

                    CommandDispatcher.Execute(cmd);
                }

                //Statistics
                var existentQuotations = QueryDispatcher.Execute(new StockQuotationsByIdQuery(stock.Id)).Count();
                var diff = existentQuotations - quotationsBefore;

                LoggingService.Info($"{diff} Quotation(s) for stock {stock.Name} imported (Qty After: {existentQuotations},Qty Before: {quotationsBefore})");
            }
        }
    }
}