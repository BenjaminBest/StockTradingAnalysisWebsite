using StockTradingAnalysis.Core.Types;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockTradingAnalysis.Web.Migration.Importer
{
	public class ImportStrategy : ImportBase
	{
        public List<StrategyDto> Items { get; set; }

		public List<StrategyDto> GetItems()
        {
            var items = new List<StrategyDto>();

            const string queryString = "SELECT [ID],[Name],[Description] FROM [dbo].[Strategies] ORDER BY [ID] ASC";
            const string countString = "SELECT COUNT([ID]) AS COUNT FROM [dbo].[Strategies]";

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
                            var item = new StrategyDto();

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
				var cmd = new StrategyAddCommand(
					item.Id,
					-1,
					item.Name,
					item.Description,
					null);

				CommandDispatcher.Execute(cmd);

				//Image
				if (ImportImage(cmd, item.OldId))
				{
					LoggingService.Info($"Stock {item.Name} ({item.OldId}) + IMAGE");
				}
				else
				{
					LoggingService.Info($"Stock {item.Name} ({item.OldId})");
				}
			}

		}

		private bool ImportImage(StrategyAddCommand addCommand, int oldId)
		{
			string queryString = $"SELECT [ID],[Data],[ContentType],[OriginalName],[Description],[RefererID],[Referer] FROM [dbo].[Images] WHERE [Referer] = 2 AND [RefererID]= {oldId}";

			Image image = null;

			//Load from db
			using (var connection = new SqlConnection(SourceConnectionString))
			{
				connection.Open();

				using (var command = new SqlCommand(queryString, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							image = new Image(addCommand.AggregateId);

							image.Data = GetImage(reader["Data"].ToString());
							image.ContentType = reader["ContentType"].ToString();
							image.OriginalName = reader["OriginalName"].ToString();
							image.Description = reader["Description"].ToString();
							break;
						}
					}
				}
			}

			if (image == null)
				return false;

			//Import
			var cmd = new StrategyChangeCommand(
				addCommand.AggregateId,
				0,
				addCommand.Name,
				addCommand.Description,
				image);

			CommandDispatcher.Execute(cmd);

			return true;
		}

		private static byte[] GetImage(string data)
		{
			var bytes = new byte[data.Length * sizeof(char)];
			Buffer.BlockCopy(data.ToCharArray(), 0, bytes, 0, bytes.Length);

			return bytes;
		}
	}
}