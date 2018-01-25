using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockNameChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockNameChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="name">The name.</param>
        public StockNameChangedEvent(Guid id, Type aggregateType, string name)
            : base(id, aggregateType)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockNameChangedEvent"/> class.
        /// </summary>
        protected StockNameChangedEvent()
        {
            
        }
    }
}