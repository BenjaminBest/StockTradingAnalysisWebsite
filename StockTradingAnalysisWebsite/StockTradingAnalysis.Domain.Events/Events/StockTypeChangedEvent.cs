using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockTypeChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the type
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockTypeChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="type">The type.</param>
        public StockTypeChangedEvent(Guid id, Type aggregateType, string type)
            : base(id, aggregateType)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockTypeChangedEvent"/> class.
        /// </summary>
        protected StockTypeChangedEvent()
        {
            
        }
    }
}