using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.Services.Domain
{
    /// <summary>
    /// The interface ITransactionPerformanceService defines a service to calculate transaction performance related information.
    /// </summary>
    public interface ITransactionPerformanceService
    {
        /// <summary>
        /// Measures the efficiency of entry in relation to the price range of the trade, 
        /// i.e. how close was the entry in relation to the lowest low of the price range of the trade
        /// </summary>
        /// <param name="mae">MAE</param>
        /// <param name="mfe">MFE</param>
        /// <param name="buyPricePerUnit">Average Price per Unit</param>
        /// <returns>Entry efficiency</returns>
        decimal? GetEntryEfficiency(decimal? mae, decimal? mfe, decimal buyPricePerUnit);

        /// <summary>
        /// Measures the efficiency of the exit in relation to the price range of the trade, 
        /// i.e. how close was the exit in relation to the highest high of the price range of the trade
        /// </summary>
        /// <param name="mae">MAE</param>
        /// <param name="mfe">MFE</param>
        /// <param name="sellPricePerUnit">Average Price per Unit</param>
        /// <returns>Entry efficiency</returns>
        decimal? GetExitEfficiency(decimal? mae, decimal? mfe, decimal sellPricePerUnit);

        /// <summary>
        /// Returns the change risk ratio = TP Points/SL Points
        /// </summary>
        /// <param name="initialTP">Take profit</param>
        /// <param name="initialSL">Stop loss</param>
        /// <param name="pricePerUnit">Price per unit</param>
        /// <param name="orderCosts">Order costs</param>
        /// <param name="units">Amount of units</param>
        decimal GetCRV(decimal initialTP, decimal initialSL, decimal pricePerUnit, decimal orderCosts, decimal units);

        /// <summary>
        /// Returns the average price per unit for the given transactions <paramref name="buyingTransactions"/>
        /// </summary>
        /// <param name="buyingTransactions">Opening Transactions</param>
        /// <returns>Average price per unit</returns>
        decimal GetAverageBuyingPricePerUnit(IEnumerable<IBuyingTransactionBookEntry> buyingTransactions);

        /// <summary>
        /// Calculates the performance for <paramref name="sellingTransaction"/>
        /// </summary>
        /// <param name="sellingTransaction">The current selling transaction</param>
        /// <param name="buyingTransactions">All buying transaction for the same stock</param>
        /// <param name="mfe">Maximum favorable excursion</param>
        /// <param name="mae">Maximum adverse excursion</param>
        /// <returns>Performance</returns>
        ITransactionPerformance GetPerformance(
            ISellingTransactionBookEntry sellingTransaction,
            IEnumerable<IBuyingTransactionBookEntry> buyingTransactions,
            decimal? mfe,
            decimal? mae);

        /// <summary>
        /// Calculates the performance for <paramref name="dividendTransaction"/>
        /// </summary>
        /// <param name="dividendTransaction">The current dividend transaction</param>
        /// <param name="buyingTransactions">All buying transaction for the same stock</param>
        /// <returns>Performance</returns>
        ITransactionPerformance GetPerformance(IDividendTransactionBookEntry dividendTransaction, IEnumerable<IBuyingTransactionBookEntry> buyingTransactions);

        /// <summary>
        /// Calculates the profit.
        /// </summary>
        /// <param name="buyingPositionSize">Size of the buying position.</param>
        /// <param name="buyingOrderCosts">The buying order costs.</param>
        /// <param name="sellingPositionSize">Size of the selling position.</param>
        /// <param name="sellingOrderCosts">The selling order costs.</param>
        /// <param name="taxes">The taxes.</param>
        /// <returns></returns>
        IProfit GetProfit(decimal buyingPositionSize, decimal buyingOrderCosts, decimal sellingPositionSize, decimal sellingOrderCosts, decimal taxes);
    }
}