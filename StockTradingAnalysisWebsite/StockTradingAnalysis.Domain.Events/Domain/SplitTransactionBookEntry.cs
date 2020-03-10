using StockTradingAnalysis.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    [DebuggerDisplay("Split {Shares} x {PricePerShare} on {OrderDate} of stock {StockId}")]
    public class SplitTransactionBookEntry : TransactionBookEntry, ISplitTransactionBookEntry
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="stockId">The id of a stock</param>
        /// <param name="transactionId">The id of a transaction</param>
        /// <param name="orderDate">The orderdate</param>
        /// <param name="shares">The amount of shares</param>
        /// <param name="pricePerShare">The price per share</param>
        public SplitTransactionBookEntry(Guid stockId, Guid transactionId, DateTime orderDate, decimal shares, decimal pricePerShare)
            : base(stockId, transactionId, orderDate, shares, pricePerShare, 0)
        {
        }

        /// <summary>
        /// Creates a copy of this instance
        /// </summary>
        /// <returns>Copy of this instance</returns>
        public new ITransactionBookEntry Copy()
        {
            return new SplitTransactionBookEntry(StockId, TransactionId, OrderDate, Shares, PricePerShare);
        }

        /// <summary>
        /// Create the new open position after the split
        /// </summary>        
        /// <param name="entries">The buying transactions before the split</param>
        /// <returns>Buying transaction book entry</returns>
        public IBuyingTransactionBookEntry CreatePositionAfterSplit(IEnumerable<ITransactionBookEntry> entries)
        {
            return new BuyingTransactionBookEntry(StockId, TransactionId, OrderDate, Shares, PricePerShare, entries.Sum(e => e.OrderCosts));
        }
    }
}