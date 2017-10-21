using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Services.Services
{
    public class TransactionPerformanceService : ITransactionPerformanceService
    {
        /// <summary>
        /// Measures the efficiency of entry in relation to the price range of the trade, 
        /// i.e. how close was the entry in relation to the lowest low of the price range of the trade
        /// </summary>
        /// <param name="mae">MAE</param>
        /// <param name="mfe">MFE</param>
        /// <param name="buyPricePerUnit">Average Price per Unit</param>
        /// <returns>Entry efficiency</returns>
        public decimal? GetEntryEfficiency(decimal? mae, decimal? mfe, decimal buyPricePerUnit)
        {
            //Long: (((MFE - Kaufpreis) / (MFE - MAE)) * 100)
            //Short: (((Kaufpreis - MAE) / (MFE - MAE()) * 100)

            if (!mae.HasValue || !mfe.HasValue)
                return null;

            return decimal.Round(((mfe.Value - buyPricePerUnit) / (mfe.Value - mae.Value)) * 100, 2);
        }

        /// <summary>
        /// Measures the efficiency of the exit in relation to the price range of the trade, 
        /// i.e. how close was the exit in relation to the highest high of the price range of the trade
        /// </summary>
        /// <param name="mae">MAE</param>
        /// <param name="mfe">MFE</param>
        /// <param name="sellPricePerUnit">Average Price per Unit</param>
        /// <returns>Entry efficiency</returns>
        public decimal? GetExitEfficiency(decimal? mae, decimal? mfe, decimal sellPricePerUnit)
        {
            //Long: (((verkaufspreis - MAE) / (MFE - MAE) )) * 100)
            //Short: (((MFE - Verkauspreis) / (MFE - MAE) )) * 100)

            if (!mae.HasValue || !mfe.HasValue || (mae.Value == sellPricePerUnit))
                return null;

            return decimal.Round(((sellPricePerUnit - mae.Value) / (mfe.Value - mae.Value)) * 100, 2);
        }

        /// <summary>
        /// Returns the change risk ratio = TP Points/SL Points
        /// </summary>
        /// <param name="initialTP">Take profit</param>
        /// <param name="initialSL">Stop loss</param>
        /// <param name="pricePerShare">Price per share</param>
        /// <param name="orderCosts">Order costs</param>
        /// <param name="shares">Amount of shares</param>
        public decimal GetCRV(decimal initialTP, decimal initialSL, decimal pricePerShare, decimal orderCosts, decimal shares)
        {
            if (initialSL == 0)
                return default(decimal);

            if (initialTP == 0)
                return default(decimal);

            if (shares == 0)
                return default(decimal);

            return decimal.Round((initialTP - pricePerShare - (orderCosts / shares)) / (pricePerShare - initialSL + (orderCosts / shares)), 2);
        }

        /// <summary>
        /// Returns the average price per unit for the given transactions <paramref name="buyingTransactions"/>
        /// </summary>
        /// <param name="buyingTransactions">Opening Transactions</param>
        /// <returns>Average price per unit</returns>
        public decimal GetAverageBuyingPricePerUnit(IEnumerable<IBuyingTransactionBookEntry> buyingTransactions)
        {
            var purchases = buyingTransactions.ToList();

            if (!purchases.Any())
                return 0.0m;

            var sumBuyingPower = purchases.Sum(t => t.PricePerShare * t.Shares);
            var sumUnits = purchases.Sum(t => t.Shares);

            return decimal.Round(sumBuyingPower / sumUnits, 2);
        }


        /// <summary>
        /// Returns the absolute MAE (maximum loss during trade) incl. order costs
        /// </summary>
        /// <remarks>If no drawdown occured (absolute no loss), 0 ist returned</remarks>
        /// <param name="mae">MAE</param>
        /// <param name="buyingTransactions">Buying transactions</param>
        /// <param name="sellOrderCosts">Ordercosts for selling</param>
        /// <returns>MAE Absolute</returns>
        public decimal? GetMAEAbsolute(decimal? mae, IEnumerable<IBuyingTransactionBookEntry> buyingTransactions, decimal sellOrderCosts)
        {
            var purchases = buyingTransactions.ToList();

            if (!mae.HasValue || purchases.Count == 0)
                return null;

            var avgPricePerUnit = GetAverageBuyingPricePerUnit(purchases);
            var units = purchases.Sum(t => t.Shares);
            var orderCosts = purchases.Sum(t => t.OrderCosts);

            var drawdown = decimal.Round(((avgPricePerUnit - mae.Value) * units) + sellOrderCosts + orderCosts, 2);

            return drawdown < 0 ? 0.0m : drawdown;
        }

        /// <summary>
        /// Returns the absolute MFE (maximum profit during trade) incl. order costs
        /// </summary>
        /// <remarks>If no profit occured (absolute no profit), 0 ist returned</remarks>
        /// <param name="mfe">MFE</param>
        /// <param name="buyingTransactions">Buying transactions</param>
        /// <param name="sellOrderCosts">Ordercosts for selling</param>
        /// <returns>MAE Absolute</returns>
        public decimal? GetMFEAbsolute(decimal? mfe, IEnumerable<IBuyingTransactionBookEntry> buyingTransactions, decimal sellOrderCosts)
        {
            var purchases = buyingTransactions.ToList();

            if (!mfe.HasValue || purchases.Count == 0)
                return null;

            var avgPricePerUnit = GetAverageBuyingPricePerUnit(purchases);
            var units = purchases.Sum(t => t.Shares);
            var orderCosts = purchases.Sum(t => t.OrderCosts);

            var maxprofit = decimal.Round(((mfe.Value - avgPricePerUnit) * units) - sellOrderCosts - orderCosts, 2);

            return maxprofit < 0 ? 0.0m : maxprofit;
        }

        /// <summary>
        /// Calculates the performance for <paramref name="sellingTransaction"/>
        /// </summary>
        /// <param name="sellingTransaction">The current selling transaction</param>
        /// <param name="buyingTransactions">All buying transaction for the same stock</param>
        /// <param name="mfe">Maximum favorable excursion</param>
        /// <param name="mae">Maximum adverse excursion</param>
        /// <returns>Performance</returns>
        public ITransactionPerformance GetPerformance(
            ISellingTransactionBookEntry sellingTransaction,
            IEnumerable<IBuyingTransactionBookEntry> buyingTransactions,
            decimal? mfe,
            decimal? mae)
        {
            var purchases = buyingTransactions.OrderBy(t => t.OrderDate).ToList();

            var buyingCosts = purchases.Sum(t => (t.PricePerShare * t.Shares) + t.OrderCosts);

            var sell = (sellingTransaction.Shares * sellingTransaction.PricePerShare) - (sellingTransaction.OrderCosts + sellingTransaction.Taxes);
            var profit = decimal.Round(sell - buyingCosts, 2);
            var percentage = decimal.Round((profit / buyingCosts) * 100, 2);
            var r = decimal.Round(profit / GetMaximumRisk(sellingTransaction.OrderDate), 2);

            var performance = new TransactionPerformance(sellingTransaction.TransactionId, profit, percentage, purchases.First().OrderDate, sellingTransaction.OrderDate, r);

            if (mfe.HasValue && mae.HasValue)
            {
                performance.EntryEfficiency = GetEntryEfficiency(mae, mfe, GetAverageBuyingPricePerUnit(purchases));
                performance.ExitEfficiency = GetExitEfficiency(mae, mfe, sellingTransaction.PricePerShare);
                performance.MAEAbsolute = GetMAEAbsolute(mae, purchases, sellingTransaction.OrderCosts);
                performance.MFEAbsolute = GetMFEAbsolute(mfe, purchases, sellingTransaction.OrderCosts);
            }

            return performance;
        }

        /// <summary>
        /// Calculates the performance for <paramref name="dividendTransaction"/>
        /// </summary>
        /// <param name="dividendTransaction">The current dividend transaction</param>
        /// <param name="buyingTransactions">All buying transaction for the same stock</param>
        /// <returns>Performance</returns>
        public ITransactionPerformance GetPerformance(IDividendTransactionBookEntry dividendTransaction, IEnumerable<IBuyingTransactionBookEntry> buyingTransactions)
        {
            var purchases = buyingTransactions.OrderBy(t => t.OrderDate).ToList();

            var buyingCosts = purchases.Sum(t => t.Shares * t.PricePerShare);

            var dividend = (dividendTransaction.Shares * dividendTransaction.PricePerShare) - (dividendTransaction.OrderCosts + dividendTransaction.Taxes);
            var profit = decimal.Round(dividend, 2);
            var percentage = decimal.Round((profit / buyingCosts) * 100, 2);
            var r = decimal.Round(profit / GetMaximumRisk(dividendTransaction.OrderDate), 2);

            var performance = new TransactionPerformance(dividendTransaction.TransactionId, profit, percentage, purchases.First().OrderDate, dividendTransaction.OrderDate, r);

            return performance;
        }

        /// <summary>
        /// Returns the maximum risk for the given order date.
        /// INFO: This is only for the migration, or a global configuration is needed
        /// </summary>
        /// <param name="orderDate">The order date</param>
        /// <returns></returns>
        private static decimal GetMaximumRisk(DateTime orderDate)
        {
            //TODO: Add MaximumRisk to global configuration

            // Bis 02.03.2013 70€
            if (orderDate.Date <= DateTime.Parse("2013-03-02").Date)
                return 70;

            // Ab  03.03.2013 60€
            if (orderDate.Date >= DateTime.Parse("2013-03-03").Date && orderDate <= DateTime.Parse("2013-06-01").Date)
                return 60;

            // Ab  01.06.2013 80€
            if (orderDate.Date >= DateTime.Parse("2013-06-01").Date && orderDate <= DateTime.Parse("2014-05-24").Date)
                return 80;

            // Ab  24.05.2014 100€
            return 100;
        }
    }
}
