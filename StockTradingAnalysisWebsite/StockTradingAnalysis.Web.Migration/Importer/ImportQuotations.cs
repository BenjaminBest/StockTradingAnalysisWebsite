using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StockTradingAnalysis.Web.Migration.Importer
{
    public class ImportQuotations : ImportBase
    {
        public IDictionary<int, IList<Quotation>> Items { get; } = new Dictionary<int, IList<Quotation>>();

        public IDictionary<int, StockDto> StockItems { get; set; }

        public void Start()
        {
            const string queryString = "SELECT [Date],[High],[Close],[Open],[Low],[Stock_ID],[Changed] FROM [dbo].[Quotations] ORDER BY [Date] ASC";
            const string countString = "SELECT COUNT([Stock_ID]) AS COUNT FROM [dbo].[Quotations]";
            const string connectionString = "Server=.;Database=TransactionManagement;User Id=stocktrading;Password=stocktrading;";

            //Load from db
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(countString, connection))
                {
                    var count = (int)command.ExecuteScalar();
                    LoggingService.Info($" ({count})");
                }

                using (var command = new SqlCommand(queryString, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new Quotation(
                                DateTime.Parse(reader["Date"].ToString()),
                                DateTime.Parse(reader["Changed"].ToString()),
                                decimal.Parse(reader["Open"].ToString()),
                                decimal.Parse(reader["Close"].ToString()),
                                decimal.Parse(reader["High"].ToString()),
                                decimal.Parse(reader["Low"].ToString()));

                            //Stock_id
                            var stockId = int.Parse(reader["Stock_ID"].ToString());

                            if (!Items.ContainsKey(stockId))
                            {
                                Items.Add(int.Parse(reader["Stock_ID"].ToString()), new List<Quotation>());
                            }

                            Items[stockId].Add(item);
                        }
                    }
                }
            }

            //Import
            LoggingService.Info("Quotations ");

            foreach (var item in Items)
            {
                var stockOld = StockItems.FirstOrDefault(s => s.Value.OldId == item.Key).Value;

                if (stockOld.IsDividend)
                    continue;

                var stock = QueryDispatcher.Execute(new StockByIdQuery(stockOld.Id));

                var version = stock.OriginalVersion;

                var cmd = new StockQuotationsAddOrChangeCommand(
                    stockOld.Id,
                    version,
                    item.Value);

                CommandDispatcher.Execute(cmd);

                LoggingService.Info($"{item.Value.Count} Quotation(s) for stock {stock.Name} ({stockOld.OldId})");
            }

        }
    }
}