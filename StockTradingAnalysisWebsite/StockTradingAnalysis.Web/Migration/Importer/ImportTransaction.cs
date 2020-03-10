using StockTradingAnalysis.Core.Types;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace StockTradingAnalysis.Web.Migration.Importer
{
	public class ImportTransaction : ImportBase
	{
        public List<ITransactionDto> Items { get; set; }

		public List<ITransactionDto> GetItems()
        {
            var items = new List<ITransactionDto>();

            const string queryString = "SELECT [ID],[OrderDate],[Units],[PricePerUnit],[OrderCosts],[Stock_ID],[ClosingTransaction],[Description],[Taxes],[InitialSL],[InitialTP],[Tag],[MFE],[MAE],[MAEAbsolute],[MFEAbsolute],[Strategy_ID] FROM [dbo].[Transactions] ORDER BY [OrderDate] ASC";
            const string countString = "SELECT COUNT([ID]) AS COUNT FROM [dbo].[Transactions]";

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
                            var isSell = bool.Parse(reader["ClosingTransaction"].ToString());
                            var oldStock = GetStock(int.Parse(reader["Stock_ID"].ToString()));

                            ITransactionDto item;


                            if (oldStock.IsDividend)
                            {
                                item = GetDividend(reader);
                            }
                            else if (isSell)
                            {
                                item = GetSelling(reader);
                            }
                            else
                            {
                                item = GetBuying(reader);
                            }

                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Gets the stock
        /// </summary>
        /// <returns></returns>
        private StockDto GetStock(int oldId)
        {
            var oldStock = StockItems.FirstOrDefault(s => s.OldId == oldId);

            // We only imported non dividend stocks, so we need to merge this now
            if (oldStock == null)
            {
                var dividendStock = DividendStockItems.FirstOrDefault(s => s.OldId == oldId);

                //Get the corresponding stock with the same WKN as the dividend
                oldStock = StockItems.FirstOrDefault(s => s.Wkn.Equals(dividendStock.Wkn));

                var dividend = (StockDto) oldStock.Clone();
                dividend.IsDividend = true;

                return dividend;
            }

            return oldStock;
        }



        public List<StockDto> StockItems { get; set; }
        public List<StockDto> DividendStockItems { get; set; }

        public List<FeedbackDto> FeedbackItems { get; set; }

		public List<StrategyDto> StrategyItems { get; set; }


		public void Start()
        {
            Items = GetItems();

            foreach(var item in Items)
            {
                if (item is DividendTransactionDto dividend)
                {
                    ImportDividend(dividend);
                }
                else if (item is SellingTransactionDto selling)
                {
                    ImportSelling(selling);
                }
                else
                {
                    ImportBuying(item as BuyingTransactionDto);
                }

                if (item.OldId == 2201)
                {
                    //Splits            
                    ImportSplit(DateTime.Parse("2012-05-16 00:00:00"), StockItems.FirstOrDefault(s => s.Wkn.Equals("548851")), 500, 1.991m); //Pferdewetten
                }

                if (item.OldId == 2184)
                {
                    //Splits            
                    ImportSplit(DateTime.Parse("2012-12-13 00:00:00"), StockItems.FirstOrDefault(s => s.Wkn.Equals("A0YJT2")), 137, 13.209124m); //Lambda TD Software
                }
            }
        }

        private DividendTransactionDto GetDividend(IDataRecord reader)
        {
            var item = new DividendTransactionDto();

            item.OldId = int.Parse(reader["ID"].ToString());
            item.Shares = decimal.Parse(reader["Units"].ToString());
            item.Description = reader["Description"].ToString();
            item.OrderCosts = decimal.Parse(reader["OrderCosts"].ToString());
            item.OrderDate = DateTime.Parse(reader["OrderDate"].ToString());
            item.OriginalVersion = -1;
            item.PricePerShare = decimal.Parse(reader["PricePerUnit"].ToString());
            item.Stock = GetStock(int.Parse(reader["Stock_ID"].ToString()));
            item.Tag = reader["Tag"].ToString();
            item.Taxes = decimal.Parse(reader["Taxes"].ToString());
            item.Image = ImportImage(item.Id, item.OldId);

            return item;
        }

        private void ImportDividend(DividendTransactionDto item)
		{
            //Import
            var cmd = new TransactionDividendCommand(
				item.Id,
				-1,
				item.OrderDate,
				item.Shares,
				item.PricePerShare,
				item.OrderCosts,
				item.Description,
				item.Tag,
				null,
				item.Stock.Id,
				item.Taxes);

			CommandDispatcher.Execute(cmd);

			//Image
			if (item.Image != null)
			{
				LoggingService.Info($"Dividend {item.Stock.Name} ({item.Shares} x {item.PricePerShare}) + IMAGE");
			}
			else
			{
				LoggingService.Info($"Dividend {item.Stock.Name} ({item.Shares} x {item.PricePerShare})");
			}
		}

        private SellingTransactionDto GetSelling(IDataRecord reader)
        {
            var item = new SellingTransactionDto();

            item.OldId = int.Parse(reader["ID"].ToString());
            item.Shares = decimal.Parse(reader["Units"].ToString());
            item.Description = reader["Description"].ToString();
            item.OrderCosts = decimal.Parse(reader["OrderCosts"].ToString());
            item.OrderDate = DateTime.Parse(reader["OrderDate"].ToString());
            item.OriginalVersion = -1;
            item.PricePerShare = decimal.Parse(reader["PricePerUnit"].ToString());
            item.Stock = GetStock(int.Parse(reader["Stock_ID"].ToString()));
            item.Tag = reader["Tag"].ToString();
            item.Taxes = decimal.Parse(reader["Taxes"].ToString());
            item.MAE = string.IsNullOrEmpty(reader["MAE"].ToString()) ? default(decimal?) : decimal.Parse(reader["MAE"].ToString());
            item.MFE = string.IsNullOrEmpty(reader["MFE"].ToString()) ? default(decimal?) : decimal.Parse(reader["MFE"].ToString());
            item.Image = ImportImage(item.Id, item.OldId);
            item.Feedback = ImportFeedback(item.OldId);

            return item;
        }

        private void ImportSelling(SellingTransactionDto item)
		{
            //Import
            var cmd = new TransactionSellCommand(
				item.Id,
				-1,
				item.OrderDate,
				item.Shares,
				item.PricePerShare,
				item.OrderCosts,
				item.Description,
				item.Tag,
				item.Image,
				item.Stock.Id,
				item.Taxes,
				item.MAE,
				item.MFE,
				item.Feedback.Select(f => f.Id));

			CommandDispatcher.Execute(cmd);

			//Image
			if (item.Image != null)
			{
				LoggingService.Info($"Sell {item.Stock.Name} ({item.Shares} x {item.PricePerShare}) + IMAGE");
			}
			else
			{
				LoggingService.Info($"Sell {item.Stock.Name} ({item.Shares} x {item.PricePerShare})");
			}
		}

		private void ImportSplit(DateTime orderDate, StockDto stock, decimal newShares, decimal newPrice)
		{
			//Import
			var cmd = new TransactionSplitCommand(
				Guid.NewGuid(),
				-1,
				orderDate,
				newShares,
				newPrice,
				stock.Id);

			CommandDispatcher.Execute(cmd);

			LoggingService.Info($"Split {stock.Name} ({newShares} x {newPrice})");
		}

        private BuyingTransactionDto GetBuying(IDataRecord reader)
        {
            var item = new BuyingTransactionDto();

            item.OldId = int.Parse(reader["ID"].ToString());
            item.Shares = decimal.Parse(reader["Units"].ToString());
            item.Description = reader["Description"].ToString();
            item.OrderCosts = decimal.Parse(reader["OrderCosts"].ToString());
            item.OrderDate = DateTime.Parse(reader["OrderDate"].ToString());
            item.OriginalVersion = -1;
            item.PricePerShare = decimal.Parse(reader["PricePerUnit"].ToString());
            item.Stock = GetStock(int.Parse(reader["Stock_ID"].ToString()));
            item.Tag = reader["Tag"].ToString();
            item.Image = ImportImage(item.Id, item.OldId);
            item.InitialSL = string.IsNullOrEmpty(reader["InitialSL"].ToString()) ? decimal.Zero : decimal.Parse(reader["InitialSL"].ToString());
            item.InitialTP = string.IsNullOrEmpty(reader["InitialTP"].ToString()) ? decimal.Zero : decimal.Parse(reader["InitialTP"].ToString());
            item.Strategy = StrategyItems.FirstOrDefault(s => s.OldId == int.Parse(reader["Strategy_ID"].ToString()));

            return item;
        }

        private void ImportBuying(BuyingTransactionDto item)
		{
            //Import
            var cmd = new TransactionBuyCommand(
				item.Id,
				-1,
				item.OrderDate,
				item.Shares,
				item.PricePerShare,
				item.OrderCosts,
				item.Description,
				item.Tag,
				item.Image,
				item.InitialSL,
				item.InitialTP,
				item.Stock.Id,
				item.Strategy.Id);

			CommandDispatcher.Execute(cmd);

			//Image
			if (item.Image != null)
			{
				LoggingService.Info($"Buy {item.Stock.Name} ({item.Shares} x {item.PricePerShare}) + IMAGE");
			}
			else
			{
				LoggingService.Info($"Buy {item.Stock.Name} ({item.Shares} x {item.PricePerShare})");
			}
		}

		private IEnumerable<IFeedback> ImportFeedback(int oldId)
		{
			string queryString = $"SELECT [ID],[FeedbackID],[TransactionsID] FROM [dbo].[TransactionsToFeedbacks] WHERE [TransactionsID] = {oldId}";

			var items = new List<IFeedback>();

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
							var id = int.Parse(reader["FeedbackID"].ToString());

							items.Add(FeedbackItems.FirstOrDefault(f=>f.OldId == id));
						}
					}
				}
			}

			return items;
		}

		private Image ImportImage(Guid id, int oldId)
		{
			string queryString = $"SELECT [ID],[Data],[ContentType],[OriginalName],[Description],[RefererID],[Referer] FROM [dbo].[Images] WHERE [Referer] = 1 AND [RefererID]= {oldId}";

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
							image = new Image(id);

							image.Data = (byte[])reader["Data"];
							image.ContentType = reader["ContentType"].ToString();
							image.OriginalName = reader["OriginalName"].ToString();
							image.Description = reader["Description"].ToString();
							break;
						}
					}
				}
			}

			return image;
		}
	}
}