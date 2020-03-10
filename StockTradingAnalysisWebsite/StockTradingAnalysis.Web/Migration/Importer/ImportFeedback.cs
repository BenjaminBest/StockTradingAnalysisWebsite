using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockTradingAnalysis.Web.Migration.Importer
{
	public class ImportFeedback : ImportBase
	{
        public List<FeedbackDto> Items  {  get; set; }

		public List<FeedbackDto> GetItems()
        {
            var items = new List<FeedbackDto>();

            const string queryString = "SELECT [ID],[Name],[Description] FROM [dbo].[Feedbacks] ORDER BY [ID] ASC";
            const string countString = "SELECT COUNT([ID]) AS COUNT FROM [dbo].[Feedbacks]";

            //Load from db
            using (var connection = new SqlConnection(SourceConnectionString))
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

                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

		public void Start()
        {
            Items = GetItems();
			//Import
			foreach (var item in Items)
			{
				var cmd = new FeedbackAddCommand(
					item.Id,
					-1,
					item.Name,
					item.Description);

				CommandDispatcher.Execute(cmd);

				LoggingService.Info($"Feedback {item.Name} ({item.OldId})");
			}

		}
	}
}
