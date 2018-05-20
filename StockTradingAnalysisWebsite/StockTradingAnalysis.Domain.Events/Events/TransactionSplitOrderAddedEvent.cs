using System;
using StockTradingAnalysis.Interfaces.Events;

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
        /// Gets the position size (amount of money put in the trade, without transaction costs)
        /// </summary>
        public decimal PositionSize { get; private set; }

        /// <summary>
        /// Gets the order costs.
        /// </summary>
        /// <value>
        /// The order costs.
        /// </value>
        public decimal OrderCosts { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSplitOrderAddedEvent" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="orderDate">The order date.</param>
        /// <param name="shares">The shares.</param>
        /// <param name="pricePerShare">The price per share.</param>
        /// <param name="positionSize">The position size.</param>
        /// <param name="orderCosts">The order costs.</param>
        /// <param name="stockId">The stock identifier.</param>
        public TransactionSplitOrderAddedEvent(Guid id, Type aggregateType,
            DateTime orderDate,
            decimal shares,
            decimal pricePerShare,
            decimal positionSize,
            decimal orderCosts,
            Guid stockId)
            : base(id, aggregateType)
        {
            OrderDate = orderDate;
            Shares = shares;
            PricePerShare = pricePerShare;
            StockId = stockId;
            PositionSize = positionSize;
            OrderCosts = orderCosts;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSplitOrderAddedEvent"/> class.
        /// </summary>
        protected TransactionSplitOrderAddedEvent()
        {

        }
    }
}