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

namespace StockTradingAnalysis.Web.Migration.Importer
{
    public class ImportTransaction : ImportBase
    {
        public IDictionary<int, ITransactionDto> Items { get; } = new Dictionary<int, ITransactionDto>();

        public IDictionary<int, StockDto> StockItems { get; set; }

        public IDictionary<int, FeedbackDto> FeedbackItems { get; set; }

        public IDictionary<int, StrategyDto> StrategyItems { get; set; }

        public void Start()
        {
            const string queryString = "SELECT [ID],[OrderDate],[Units],[PricePerUnit],[OrderCosts],[Stock_ID],[ClosingTransaction],[Description],[Taxes],[InitialSL],[InitialTP],[Tag],[MFE],[MAE],[MAEAbsolute],[MFEAbsolute],[Strategy_ID] FROM [dbo].[Transactions] ORDER BY [OrderDate] ASC";
            const string countString = "SELECT COUNT([ID]) AS COUNT FROM [dbo].[Transactions]";
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
                            var isSell = bool.Parse(reader["ClosingTransaction"].ToString());
                            var isDividend = StockItems[int.Parse(reader["Stock_ID"].ToString())].IsDividend;

                            ITransactionDto item;

                            if (isDividend)
                            {
                                item = ImportDividend(reader);
                            }
                            else if (isSell)
                            {
                                //Custom handling for splits
                                var orderDate = DateTime.Parse(reader["OrderDate"].ToString());

                                //Pferdewetten
                                if (int.Parse(reader["ID"].ToString()) == 2056)
                                {
                                    ImportSplit(reader, 500, new decimal(1.991));
                                }


                                item = ImportSelling(reader);
                            }
                            else
                            {
                                item = ImportBuying(reader);
                            }

                            Items.Add(item.OldId, item);
                        }
                    }
                }
            }
        }

        private ITransactionDto ImportDividend(IDataRecord reader)
        {
            var item = new DividendTransactionDto();

            var dividendStock = StockItems[int.Parse(reader["Stock_ID"].ToString())];

            item.OldId = int.Parse(reader["ID"].ToString());
            item.Shares = decimal.Parse(reader["Units"].ToString());
            item.Description = reader["Description"].ToString();
            item.OrderCosts = decimal.Parse(reader["OrderCosts"].ToString());
            item.OrderDate = DateTime.Parse(reader["OrderDate"].ToString());
            item.OriginalVersion = -1;
            item.PricePerShare = decimal.Parse(reader["PricePerUnit"].ToString());
            item.Stock = StockItems.FirstOrDefault(s => s.Value.Wkn.Equals(dividendStock.Wkn) && s.Value.OldId != dividendStock.OldId).Value;
            item.Tag = reader["Tag"].ToString();
            item.Taxes = decimal.Parse(reader["Taxes"].ToString());
            item.Image = ImportImage(item.Id, item.OldId);

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

            CommandDispatcher.Execute(new TransactionCalculateDividendCommand(item.Id, 0));

            //Image
            if (item.Image != null)
            {
                LoggingService.Info($"Dividend {item.Stock.Name} ({item.Shares} x {item.PricePerShare}) + IMAGE");
            }
            else
            {
                LoggingService.Info($"Dividend {item.Stock.Name} ({item.Shares} x {item.PricePerShare})");
            }

            return item;
        }

        private ITransactionDto ImportSelling(IDataRecord reader)
        {
            var item = new SellingTransactionDto();

            item.OldId = int.Parse(reader["ID"].ToString());
            item.Shares = decimal.Parse(reader["Units"].ToString());
            item.Description = reader["Description"].ToString();
            item.OrderCosts = decimal.Parse(reader["OrderCosts"].ToString());
            item.OrderDate = DateTime.Parse(reader["OrderDate"].ToString());
            item.OriginalVersion = -1;
            item.PricePerShare = decimal.Parse(reader["PricePerUnit"].ToString());
            item.Stock = StockItems[int.Parse(reader["Stock_ID"].ToString())];
            item.Tag = reader["Tag"].ToString();
            item.Taxes = decimal.Parse(reader["Taxes"].ToString());
            item.MAE = string.IsNullOrEmpty(reader["MAE"].ToString()) ? default(decimal?) : decimal.Parse(reader["MAE"].ToString());
            item.MFE = string.IsNullOrEmpty(reader["MFE"].ToString()) ? default(decimal?) : decimal.Parse(reader["MFE"].ToString());
            item.Image = ImportImage(item.Id, item.OldId);
            item.Feedback = ImportFeedback(item.OldId);

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

            CommandDispatcher.Execute(new TransactionCalculateCommand(item.Id, 0));

            //Image
            if (item.Image != null)
            {
                LoggingService.Info($"Sell {item.Stock.Name} ({item.Shares} x {item.PricePerShare}) + IMAGE");
            }
            else
            {
                LoggingService.Info($"Sell {item.Stock.Name} ({item.Shares} x {item.PricePerShare})");
            }

            return item;
        }

        private void ImportSplit(IDataRecord reader, decimal newShares, decimal newPrice)
        {
            var orderDate = DateTime.Parse(reader["OrderDate"].ToString());
            orderDate = orderDate.AddDays(-1); //Split should be happened before sell

            var stock = StockItems[int.Parse(reader["Stock_ID"].ToString())];

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

        private ITransactionDto ImportBuying(IDataRecord reader)
        {
            var item = new BuyingTransactionDto();

            item.OldId = int.Parse(reader["ID"].ToString());
            item.Shares = decimal.Parse(reader["Units"].ToString());
            item.Description = reader["Description"].ToString();
            item.OrderCosts = decimal.Parse(reader["OrderCosts"].ToString());
            item.OrderDate = DateTime.Parse(reader["OrderDate"].ToString());
            item.OriginalVersion = -1;
            item.PricePerShare = decimal.Parse(reader["PricePerUnit"].ToString());
            item.Stock = StockItems[int.Parse(reader["Stock_ID"].ToString())];
            item.Tag = reader["Tag"].ToString();
            item.Image = ImportImage(item.Id, item.OldId);
            item.InitialSL = string.IsNullOrEmpty(reader["InitialSL"].ToString()) ? decimal.Zero : decimal.Parse(reader["InitialSL"].ToString());
            item.InitialTP = string.IsNullOrEmpty(reader["InitialTP"].ToString()) ? decimal.Zero : decimal.Parse(reader["InitialTP"].ToString());
            item.Strategy = StrategyItems[int.Parse(reader["Strategy_ID"].ToString())];

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

            return item;
        }

        private IEnumerable<IFeedback> ImportFeedback(int oldId)
        {
            string queryString = $"SELECT [ID],[FeedbackID],[TransactionsID] FROM [dbo].[TransactionsToFeedbacks] WHERE [TransactionsID] = {oldId}";
            const string connectionString = "Server=.;Database=TransactionManagement;User Id=stocktrading;Password=stocktrading;";

            var items = new List<IFeedback>();

            //Load from db
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(queryString, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["FeedbackID"].ToString());

                            items.Add(FeedbackItems[id]);
                        }
                    }
                }
            }

            return items;
        }

        private static Image ImportImage(Guid id, int oldId)
        {
            string queryString = $"SELECT [ID],[Data],[ContentType],[OriginalName],[Description],[RefererID],[Referer] FROM [dbo].[Images] WHERE [Referer] = 1 AND [RefererID]= {oldId}";
            const string connectionString = "Server=.;Database=TransactionManagement;User Id=stocktrading;Password=stocktrading;";

            Image image = null;

            //Load from db
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(queryString, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            image = new Image(id);

                            image.Data = GetImage(reader["Data"].ToString());
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

        private static byte[] GetImage(string data)
        {
            var bytes = new byte[data.Length * sizeof(char)];
            Buffer.BlockCopy(data.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}