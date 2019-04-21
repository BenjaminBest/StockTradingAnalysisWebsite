using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StockTradingAnalysis.Web.Migration.Importer
{
	public class ImportCalculations : ImportBase
	{
		public IDictionary<int, CalculationDto> Items { get; } = new Dictionary<int, CalculationDto>();

		public void Start()
		{
			const string queryString = "SELECT [ID],[Name],[WKN],[Multiplier],[StrikePrice],[Underlying],[InitialSL],[InitialTP],[PricePerUnit],[OrderCosts],[Description],[Units],[IsLong] FROM [dbo].[Calculations] ORDER BY [ID] ASC";
			const string countString = "SELECT COUNT([ID]) AS COUNT FROM [dbo].[Calculations]";

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
							var item = new CalculationDto();

							item.OldId = int.Parse(reader["ID"].ToString());
							item.Name = reader["Name"].ToString();
							item.Wkn = reader["WKN"].ToString();
							item.Multiplier = decimal.Parse(reader["Multiplier"].ToString());
							item.StrikePrice = decimal.Parse(reader["StrikePrice"].ToString());
							item.Underlying = reader["Underlying"].ToString();
							item.InitialSl = decimal.Parse(reader["InitialSL"].ToString());
							item.InitialTp = decimal.Parse(reader["InitialTP"].ToString());
							item.PricePerUnit = decimal.Parse(reader["PricePerUnit"].ToString());
							item.OrderCosts = decimal.Parse(reader["OrderCosts"].ToString());
							item.Description = reader["Description"].ToString();
							item.Units = decimal.Parse(reader["Units"].ToString());
							item.IsLong = bool.Parse(reader["IsLong"].ToString());

							Items.Add(item.OldId, item);
						}
					}
				}
			}

			//Import
			foreach (var item in Items)
			{
				var cmd = new CalculationAddCommand(
					item.Value.Id,
					-1,
					item.Value.Name,
					item.Value.Wkn,
					item.Value.Multiplier,
					item.Value.StrikePrice,
					item.Value.Underlying,
					item.Value.InitialSl,
					item.Value.InitialTp,
					item.Value.PricePerUnit,
					item.Value.OrderCosts,
					item.Value.Description,
					item.Value.Units,
					item.Value.IsLong);

				CommandDispatcher.Execute(cmd);

				LoggingService.Info($"Calculation {item.Value.Name} ({item.Value.OldId})");
			}

		}
	}
}
