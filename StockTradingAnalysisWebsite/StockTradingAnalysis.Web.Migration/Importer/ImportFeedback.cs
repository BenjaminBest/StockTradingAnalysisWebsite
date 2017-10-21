using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockTradingAnalysis.Web.Migration.Importer
{
    public class ImportFeedback : ImportBase
    {
        public IDictionary<int, FeedbackDto> Items { get; } = new Dictionary<int, FeedbackDto>();

        public void Start()
        {
            const string queryString = "SELECT [ID],[Name],[Description] FROM [dbo].[Feedbacks] ORDER BY [ID] ASC";
            const string countString = "SELECT COUNT([ID]) AS COUNT FROM [dbo].[Feedbacks]";
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
                            var item = new FeedbackDto();

                            item.OldId = int.Parse(reader["ID"].ToString());
                            item.Name = reader["Name"].ToString();
                            item.Description = reader["Description"].ToString();

                            Items.Add(item.OldId, item);
                        }
                    }
                }
            }

            //Import
            foreach (var item in Items)
            {
                var cmd = new FeedbackAddCommand(
                    item.Value.Id,
                    -1,
                    item.Value.Name,
                    item.Value.Description);

                CommandDispatcher.Execute(cmd);

                LoggingService.Info($"Feedback {item.Value.Name} ({item.Value.OldId})");
            }

        }
    }
}
