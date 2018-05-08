using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class TransactionUndoEvent : DomainEvent
    {
        /// <summary>
        /// Gets the order date
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionUndoEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="orderDate"></param>
        public TransactionUndoEvent(Guid id, Type aggregateType, DateTime orderDate)
            : base(id, aggregateType)
        {
            OrderDate = orderDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionUndoEvent"/> class.
        /// </summary>
        protected TransactionUndoEvent()
        {

        }
    }
}