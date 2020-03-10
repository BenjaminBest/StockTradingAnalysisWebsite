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
		public List<ITransactionDto> Transactions { get; set; }

		public TestPerformance(List<ITransactionDto> transactions)
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
				if (transaction is SellingTransactionDto sellingTransaction)
				{
					query = QueryDispatcher.Execute(new TransactionPerformanceByIdQuery(sellingTransaction.Id));
					date = sellingTransaction.OrderDate;
					desc = $"{sellingTransaction.Stock.Name} ({sellingTransaction.Shares} x {sellingTransaction.PricePerShare})";
				}

				//Dividend
				if (transaction is DividendTransactionDto dividendTransaction)
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

				//Already checked and results of new implementation are correct
				if (!new List<int>()
				{

				}.Contains(stat.Key))
				{
					//Comparison                
					if (query.ProfitAbsolute.Equals(statistics.ProfitAbsolute) &&
						!query.ProfitPercentage.Equals(statistics.ProfitPercentage))
					{
						//Absolute profit is equal, but percentage is not
						LoggingService.Warn(
							$"{date} Statistics: {stat.Key} has same abs. Profit but diff. Percentage ({query.ProfitPercentage} vs {statistics.ProfitPercentage}) / {desc}");
						errorOccured = true;
					}
					else
					{
						if (!query.ProfitAbsolute.Equals(statistics.ProfitAbsolute))
						{
							LoggingService.Warn(
								$"{date} Statistics: {stat.Key} has different ProfitAbsolute ({query.ProfitAbsolute} vs {statistics.ProfitAbsolute}) / {desc}");
							errorOccured = true;
						}

						if (!query.ProfitPercentage.Equals(statistics.ProfitPercentage))
						{
							LoggingService.Warn(
								$"{date} Statistics: {stat.Key} has different ProfitPercentage ({query.ProfitPercentage} vs {statistics.ProfitPercentage}) / {desc}");
							errorOccured = true;
						}
					}

					if (!query.EntryEfficiency.Equals(statistics.EntryEfficiency))
					{
						LoggingService.Warn(
							$"{date} Statistics: {stat.Key} has different EntryEfficiency ({query.EntryEfficiency} vs {statistics.EntryEfficiency}) / {desc}");
						errorOccured = true;
					}

					if (!query.ExitEfficiency.Equals(statistics.ExitEfficiency))
					{
						LoggingService.Warn(
							$"{date} Statistics: {stat.Key} has different ExitEfficiency ({query.ExitEfficiency} vs {statistics.ExitEfficiency}) / {desc}");
						errorOccured = true;
					}
				}

				if (!errorOccured)
					LoggingService.Info($"{date} Statistics: {stat.Key} is fine");
			}
		}

		private IDictionary<int, StatisticsDto> GetStatistics()
		{
			const string queryString =
				"SELECT MAX([ID]) AS [ID],MAX([ProfitAbsolute]) AS [ProfitAbsolute], MAX([ProfitPercentage]) AS [ProfitPercentage],MAX([R]) AS [R],MAX([EntryEfficiency]) AS [EntryEfficiency],\r\nMAX([ExitEfficiency]) AS [ExitEfficiency],MAX([TransactionEndID]) AS [TransactionEndID] FROM [dbo].[Statistics] \r\nGROUP BY [TransactionEndID]\r\nHAVING [TransactionEndID] IS NOT NULL AND MAX([ProfitAbsolute]) IS NOT NULL \r\nORDER BY [ID] ASC;";

			IDictionary<int, StatisticsDto> statistics = new Dictionary<int, StatisticsDto>();

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
