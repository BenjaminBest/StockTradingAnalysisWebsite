using Newtonsoft.Json;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StockTradingAnalysis.Web.Migration.Importer
{
	public class ImportStock : ImportBase
	{
        public List<StockDto> Items { get; set; }

        public List<StockDto> DividendItems { get; set; }


        public List<StockDto> GetItems()
        {
            var items = new List<StockDto>();

            const string queryString = "SELECT [ID],[WKN],[Name],[Type],[IsDividend],[LongShort] FROM [dbo].[Stocks] ORDER BY [ID] ASC";
            const string countString = "SELECT COUNT([ID]) AS COUNT FROM [dbo].[Stocks]";

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
                            var item = new StockDto(Guid.NewGuid());

                            item.OldId = int.Parse(reader["ID"].ToString());
                            item.IsDividend = bool.Parse(reader["IsDividend"].ToString());
                            item.LongShort = reader["LongShort"].ToString();
                            item.Name = reader["Name"].ToString();
                            item.Type = reader["Type"].ToString();
                            item.Wkn = reader["WKN"].ToString();

                            items.Add(item);
                        }
                    }
                }
            }


            return items;
        }


		public void Start()
        {
            Items = GetItems().Where(i => !i.IsDividend).ToList();
            DividendItems = GetItems().Where(i => i.IsDividend).ToList();

            //Import
            foreach (var item in Items)
			{
                var cmd = new StockAddCommand(
					item.Id,
					-1,
					item.Name,
					item.Wkn,
					item.Type,
					item.LongShort);

				CommandDispatcher.Execute(cmd);

				LoggingService.Info($"Stock {item.Name} (Old ID:{item.OldId}, New ID:{item.Id})");
			}

		}
	}
}
