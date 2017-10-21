using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Defines an bucket entry of the transaction book
    /// </summary>
    public class TransactionBookEntry : ITransactionBookEntry
    {
        /// <summary>
        /// Gets the if of a stock
        /// </summary>
        public Guid StockId { get; }

        /// <summary>
        /// Gets the id of a transaction
        /// </summary>
        public Guid TransactionId { get; }

        /// <summary>
        /// Gets the order date
        /// </summary>
        public DateTime OrderDate { get; }

        /// <summary>
        /// Gets/sets the amount of units
        /// </summary>
        public decimal Shares { get; set; }

        /// <summary>
        /// Gets the price per unit
        /// </summary>
        public decimal PricePerShare { get; }

        /// <summary>
        /// Gets the order costs
        /// </summary>
        public decimal OrderCosts { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="stockId">The id of a stock</param>
        /// <param name="transactionId">The id of a transaction</param>
        /// <param name="orderDate">The orderdate</param>
        /// <param name="units">The amount of units</param>
        /// <param name="pricePerUnit">The price per share</param>
        /// <param name="orderCosts">The order costs</param>
        public TransactionBookEntry(Guid stockId, Guid transactionId, DateTime orderDate, decimal units, decimal pricePerUnit, decimal orderCosts)
        {
            StockId = stockId;
            TransactionId = transactionId;
            OrderDate = orderDate;
            Shares = units;
            PricePerShare = pricePerUnit;
            OrderCosts = orderCosts;
        }

        /// <summary>
        /// Creates a copy of this instance
        /// </summary>
        /// <returns>Copy of this instance</returns>
        public ITransactionBookEntry Copy()
        {
            return new TransactionBookEntry(StockId, TransactionId, OrderDate, Shares, PricePerShare, OrderCosts);
        }

    }
}
