using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Web.Migration.Common;
using System.Linq;

namespace StockTradingAnalysis.Web.Migration.Importer
{
    public class TestQueries : ImportBase
    {
        public void Start()
        {
            //Stocks
            var stocks = QueryDispatcher.Execute(new StockAllQuery()).ToList();
            LoggingService.Info($"Found {stocks.Count()} stocks");

            foreach (var item in stocks)
            {
                var query = QueryDispatcher.Execute(new StockByIdQuery(item.Id));
            }

            //Quotations
            var sumQuotations = 0;
            foreach (var item in stocks)
            {
                sumQuotations += QueryDispatcher.Execute(new StockQuotationsCountByIdQuery(item.Id));
            }
            LoggingService.Info($"Found {sumQuotations} quotations");

            //Feedback
            var feedbacks = QueryDispatcher.Execute(new FeedbackAllQuery()).ToList();
            LoggingService.Info($"Found {feedbacks.Count()} feedbacks");

            foreach (var item in feedbacks)
            {
                var query = QueryDispatcher.Execute(new FeedbackByIdQuery(item.Id));
            }

            //Calculations
            var calculations = QueryDispatcher.Execute(new CalculationAllQuery()).ToList();
            LoggingService.Info($"Found {calculations.Count()} calculations");

            foreach (var item in calculations)
            {
                var query = QueryDispatcher.Execute(new CalculationByIdQuery(item.Id));
            }

            //Strategies
            var strategies = QueryDispatcher.Execute(new StrategyAllQuery()).ToList();
            LoggingService.Info($"Found {strategies.Count()} strategies");

            foreach (var item in strategies)
            {
                var query = QueryDispatcher.Execute(new StrategyByIdQuery(item.Id));
            }

            //Transactions
            var transactions = QueryDispatcher.Execute(new TransactionAllQuery()).ToList();
            LoggingService.Info($"Found {transactions.Count()} transactions");

            foreach (var item in transactions)
            {
                var query = QueryDispatcher.Execute(new TransactionByIdQuery(item.Id));

                //TODO: Test if every translation was found and no null was returned
            }

            //Performances
            var performance = QueryDispatcher.Execute(new TransactionPerformanceAllQuery()).ToList();
            LoggingService.Info($"Found {performance.Count()} performances");

            foreach (var item in performance)
            {
                var query = QueryDispatcher.Execute(new TransactionPerformanceByIdQuery(item.Id));
            }
        }
    }
}
