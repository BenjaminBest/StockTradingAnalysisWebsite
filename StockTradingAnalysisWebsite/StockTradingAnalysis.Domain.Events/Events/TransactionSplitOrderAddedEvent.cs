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

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSplitOrderAddedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="orderDate">The order date.</param>
        /// <param name="shares">The shares.</param>
        /// <param name="pricePerShare">The price per share.</param>
        /// <param name="stockId">The stock identifier.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSplitOrderAddedEvent"/> class.
        /// </summary>
        protected TransactionSplitOrderAddedEvent()
        {
            
        }
    }
}