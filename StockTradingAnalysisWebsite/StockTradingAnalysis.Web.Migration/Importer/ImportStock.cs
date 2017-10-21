using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StockTradingAnalysis.Web.Migration.Importer
{
    public class ImportStock : ImportBase
    {
        public IDictionary<int, StockDto> Items { get; } = new Dictionary<int, StockDto>();

        public void Start()
        {
            const string queryString = "SELECT [ID],[WKN],[Name],[Type],[IsDividend],[LongShort] FROM [dbo].[Stocks] ORDER BY [ID] ASC";
            const string countString = "SELECT COUNT([ID]) AS COUNT FROM [dbo].[Stocks]";
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
                            var item = new StockDto();

                            item.OldId = int.Parse(reader["ID"].ToString());
                            item.IsDividend = bool.Parse(reader["IsDividend"].ToString());
                            item.LongShort = reader["LongShort"].ToString();
                            item.Name = reader["Name"].ToString();
                            item.Type = reader["Type"].ToString();
                            item.Wkn = reader["WKN"].ToString();

                            Items.Add(item.OldId, item);
                        }
                    }
                }
            }

            //Import
            foreach (var item in Items.Where(i => !i.Value.IsDividend))
            {
                var cmd = new StockAddCommand(
                    item.Value.Id,
                    -1,
                    item.Value.Name,
                    item.Value.Wkn,
                    item.Value.Type,
                    item.Value.LongShort);

                CommandDispatcher.Execute(cmd);

                LoggingService.Info($"Stock {item.Value.Name} ({item.Value.OldId})");
            }

        }
    }
}
