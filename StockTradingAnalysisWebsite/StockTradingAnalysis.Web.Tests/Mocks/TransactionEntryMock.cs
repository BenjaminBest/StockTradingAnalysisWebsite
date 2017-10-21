using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Domain;
using System;

namespace StockTradingAnalysis.Web.Tests.Mocks
{
    public class TransactionEntryMock
    {
        public static ISellingTransactionBookEntry CreateSelling(Guid stockId, decimal units)
        {
            return new SellingTransactionBookEntry(stockId, Guid.NewGuid(), DateTime.Now, units, 0, 0, 0);
        }

        public static ISellingTransactionBookEntry CreateSelling(Guid stockId, decimal shares, decimal pricePerShare, decimal orderCosts)
        {
            return new SellingTransactionBookEntry(stockId, Guid.NewGuid(), DateTime.Now, shares, pricePerShare, orderCosts, 0);
        }

        public static ISellingTransactionBookEntry CreateSelling(Guid stockId, Guid transactionId, decimal shares, decimal pricePerShare, decimal orderCosts, decimal taxes, DateTime orderDate)
        {
            return new SellingTransactionBookEntry(stockId, transactionId, orderDate, shares, pricePerShare, orderCosts, taxes);
        }

        public static IBuyingTransactionBookEntry CreateBuying(Guid stockId, decimal units)
        {
            return new BuyingTransactionBookEntry(stockId, Guid.NewGuid(), DateTime.Now, units, 0, 0);

        }

        public static IBuyingTransactionBookEntry CreateBuying(Guid stockId, decimal shares, decimal pricePerShare, decimal orderCosts)
        {
            return new BuyingTransactionBookEntry(stockId, Guid.NewGuid(), DateTime.Now, shares, pricePerShare, orderCosts);

        }

        public static IBuyingTransactionBookEntry CreateBuying(Guid stockId, decimal shares, decimal pricePerShare, decimal orderCosts, DateTime orderDate)
        {
            return new BuyingTransactionBookEntry(stockId, Guid.NewGuid(), orderDate, shares, pricePerShare, orderCosts);

        }

        public static IDividendTransactionBookEntry CreateDividend(Guid stockId, decimal units)
        {
            return new DividendTransactionBookEntry(stockId, Guid.NewGuid(), DateTime.Now, units, 0, 0, 0);
        }

        public static IDividendTransactionBookEntry CreateDividend(Guid stockId, Guid transactionId, decimal shares, decimal pricePerShare, decimal orderCosts, decimal taxes, DateTime orderDate)
        {
            return new DividendTransactionBookEntry(stockId, transactionId, orderDate, shares, pricePerShare, orderCosts, taxes);
        }

        public static ISplitTransactionBookEntry CreateSplit(Guid stockId, decimal units)
        {
            return new SplitTransactionBookEntry(stockId, Guid.NewGuid(), DateTime.Now, units, 0);
        }

        public static ISplitTransactionBookEntry CreateSplit(Guid stockId, Guid transactionId, decimal shares, decimal pricePerShare, DateTime orderDate)
        {
            return new SplitTransactionBookEntry(stockId, transactionId, orderDate, shares, pricePerShare);
        }
    }
}