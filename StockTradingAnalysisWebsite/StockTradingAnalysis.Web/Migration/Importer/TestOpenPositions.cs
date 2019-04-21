using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StockTradingAnalysis.Web.Migration.Importer
{
	public class TestOpenPositions : ImportBase
	{
		public IDictionary<int, StockDto> Stocks { get; set; }
		public IDictionary<int, ITransactionDto> Transactions { get; set; }

		public TestOpenPositions(IDictionary<int, StockDto> stocks, IDictionary<int, ITransactionDto> transactions)
		{
			Stocks = stocks;
			Transactions = transactions;
		}

		public void Start()
		{
			//Get old transactions
			var transactions = GetOpenPositions(Stocks);

			var positions = QueryDispatcher.Execute(new OpenPositionsAllQuery());

			foreach (var pos in positions)
			{
				var oldOpenTrans = transactions.Values.Where(t => t.Stock.Id == pos.ProductId).ToList();

				if (!oldOpenTrans.Any())
				{
					LoggingService.Warn($"Unknown position of {pos.Shares}x{pos.PricePerShare}");
				}
				else if (pos.Shares != oldOpenTrans.Sum(t => t.Shares))
				{
					LoggingService.Warn($"Diffent amount of shares for {oldOpenTrans.FirstOrDefault().Stock.Name} {pos.Shares}x{pos.PricePerShare}");
				}
				else
				{
					LoggingService.Info($"Open position ok for {oldOpenTrans.FirstOrDefault().Stock.Name} ({pos.Shares}x{pos.PricePerShare}={pos.PositionSize})");
				}
			}
		}

		private IDictionary<int, BuyingTransactionDto> GetOpenPositions(IDictionary<int, StockDto> stocks)
		{
			const string queryString = "SELECT T.[ID], T.[OrderCosts], T.[OrderDate], T.[PricePerUnit], T.[Units],T.[Stock_ID] FROM [TransactionManagement].[dbo].[Statistics] S LEFT JOIN [TransactionManagement].[dbo].[Transactions] T ON S.[TransactionStartID] = T.[ID]  WHERE [TransactionEndID] IS NULL ORDER BY T.[OrderDate] ASC";

			IDictionary<int, BuyingTransactionDto> buys = new Dictionary<int, BuyingTransactionDto>();

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
							var item = new BuyingTransactionDto();

							item.OldId = int.Parse(reader["ID"].ToString());
							item.Shares = decimal.Parse(reader["Units"].ToString());
							item.PricePerShare = decimal.Parse(reader["PricePerUnit"].ToString());
							item.OrderDate = DateTime.Parse(reader["OrderDate"].ToString());
							item.OrderCosts = decimal.Parse(reader["OrderCosts"].ToString());
							item.Stock = stocks[int.Parse(reader["Stock_ID"].ToString())];

							buys.Add(item.OldId, item);
						}
					}
				}
			}

			return buys;
		}
	}
}
