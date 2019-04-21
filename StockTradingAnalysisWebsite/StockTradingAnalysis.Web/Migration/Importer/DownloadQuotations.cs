using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Services.External.Services;
using StockTradingAnalysis.Web.Common.Services;
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

                var quotations = new QuotationServiceClient(QueryDispatcher, new StockQuoteExternalService(LoggingService)).Get(stock.Id).ToList();

                if (!quotations.Any())
                {
                    LoggingService.Info($"No quotations for stock {stock.Name} imported (Qty Before: {quotationsBefore})");
                    continue;
                }

                var cmd = new StockQuotationsAddOrChangeCommand(
                    stock.Id,
                    stock.OriginalVersion,
                    quotations);

                CommandDispatcher.Execute(cmd);

                //Statistics
                var existentQuotations = QueryDispatcher.Execute(new StockQuotationsByIdQuery(stock.Id)).Count();
                var diff = existentQuotations - quotationsBefore;

                LoggingService.Info($"{diff} Quotation(s) for stock {stock.Name} imported (Qty After: {existentQuotations},Qty Before: {quotationsBefore})");
            }
        }
    }
}