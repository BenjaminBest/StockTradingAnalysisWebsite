using StockTradingAnalysis.Interfaces.Events;
using System;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class TransactionSplitOrderAddedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the order date
        /// </summary>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Gets the amount of shares
        /// </summary>
        public decimal Shares { get; private set; }

        /// <summary>
        /// Gets the price per unit
        /// </summary>
        public decimal PricePerShare { get; private set; }

        /// <summary>
        /// Gets the underlying stock
        /// </summary>
        public Guid StockId { get; private set; }

        public TransactionSplitOrderAddedEvent(Guid id, Type aggregateType,
            DateTime orderDate,
            decimal shares,
            decimal pricePerShare,
            Guid stockId)
            : base(id, aggregateType)
        {
            OrderDate = orderDate;
            Shares = shares;
            PricePerShare = pricePerShare;
            StockId = stockId;
        }
    }
}