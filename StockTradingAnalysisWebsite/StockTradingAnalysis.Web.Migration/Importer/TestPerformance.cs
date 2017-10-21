using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockTradingAnalysis.Web.Migration.Importer
{
    public class TestPerformance : ImportBase
    {
        public IDictionary<int, ITransactionDto> Transactions { get; set; }

        public TestPerformance(IDictionary<int, ITransactionDto> transactions)
        {
            Transactions = transactions;
        }

        public void Start()
        {
            //Get old statistics
            var stats = GetStatistics();


            foreach (var stat in stats)
            {
                var statistics = stat.Value;
                var transaction = Transactions[statistics.OldTransactionEndId];

                var errorOccured = false;
                ITransactionPerformance query = null;
                var date = DateTime.MinValue;
                var desc = string.Empty;

                //Selling
                var sellingTransaction = transaction as SellingTransactionDto;
                if (sellingTransaction != null)
                {
                    query = QueryDispatcher.Execute(new TransactionPerformanceByIdQuery(sellingTransaction.Id));
                    date = sellingTransaction.OrderDate;
                    desc = $"{sellingTransaction.Stock.Name} ({sellingTransaction.Shares} x {sellingTransaction.PricePerShare})";
                }

                //Dividend
                var dividendTransaction = transaction as DividendTransactionDto;
                if (dividendTransaction != null)
                {
                    query = QueryDispatcher.Execute(new TransactionPerformanceByIdQuery(dividendTransaction.Id));
                    date = dividendTransaction.OrderDate;
                    desc = $"{dividendTransaction.Stock.Name} ({dividendTransaction.Shares} x {dividendTransaction.PricePerShare})";
                }

                if (query == null)
                {
                    LoggingService.Warn($"Statistics: {stat.Key} not found");
                    continue;
                }

                //Comparison
                if (!query.ProfitAbsolute.Equals(statistics.ProfitAbsolute))
                {
                    LoggingService.Warn($"{date} Statistics: {stat.Key} has different ProfitAbsolute ({query.ProfitAbsolute} vs {statistics.ProfitAbsolute}) / {desc}");
                    errorOccured = true;
                }

                if (!query.ProfitPercentage.Equals(statistics.ProfitPercentage))
                {
                    LoggingService.Warn($"{date} Statistics: {stat.Key} has different ProfitPercentage ({query.ProfitPercentage} vs {statistics.ProfitPercentage}) / {desc}");
                    errorOccured = true;
                }

                if (!query.R.Equals(statistics.R))
                {
                    LoggingService.Warn($"{date} Statistics: {stat.Key} has different R ({query.R} vs {statistics.R}) / {desc}");
                    errorOccured = true;
                }

                if (!query.EntryEfficiency.Equals(statistics.EntryEfficiency))
                {
                    LoggingService.Warn($"{date} Statistics: {stat.Key} has different EntryEfficiency ({query.EntryEfficiency} vs {statistics.EntryEfficiency}) / {desc}");
                    errorOccured = true;
                }

                if (!query.ExitEfficiency.Equals(statistics.ExitEfficiency))
                {
                    LoggingService.Warn($"{date} Statistics: {stat.Key} has different ExitEfficiency ({query.ExitEfficiency} vs {statistics.ExitEfficiency}) / {desc}");
                    errorOccured = true;
                }

                if (!errorOccured)
                    LoggingService.Info($"{date} Statistics: {stat.Key} is fine");
            }
        }

        private static IDictionary<int, StatisticsDto> GetStatistics()
        {
            const string queryString = "SELECT [ID],[ProfitAbsolute],[ProfitPercentage],[R],[EntryEfficiency],[ExitEfficiency],[TransactionEndID] FROM [dbo].[Statistics] WHERE [TransactionEndID] IS NOT NULL AND [ProfitAbsolute] IS NOT NULL ORDER BY [ID] ASC";
            const string connectionString = "Server=.;Database=TransactionManagement;User Id=stocktrading;Password=stocktrading;";

            IDictionary<int, StatisticsDto> statistics = new Dictionary<int, StatisticsDto>();

            //Load from db
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(queryString, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new StatisticsDto();

                            item.OldId = int.Parse(reader["ID"].ToString());
                            item.OldTransactionEndId = int.Parse(reader["TransactionEndID"].ToString());
                            item.ProfitAbsolute = decimal.Parse(reader["ProfitAbsolute"].ToString());
                            item.ProfitPercentage = decimal.Parse(reader["ProfitPercentage"].ToString());
                            item.R = decimal.Parse(reader["R"].ToString());
                            item.EntryEfficiency = string.IsNullOrEmpty(reader["EntryEfficiency"].ToString()) ? default(decimal?) : decimal.Parse(reader["EntryEfficiency"].ToString());
                            item.ExitEfficiency = string.IsNullOrEmpty(reader["ExitEfficiency"].ToString()) ? default(decimal?) : decimal.Parse(reader["ExitEfficiency"].ToString());

                            statistics.Add(item.OldId, item);
                        }
                    }
                }
            }

            return statistics;
        }
    }
}
