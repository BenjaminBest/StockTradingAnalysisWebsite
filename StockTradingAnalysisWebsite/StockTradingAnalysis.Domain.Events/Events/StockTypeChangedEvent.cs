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

        public StockTypeChangedEvent(Guid id, Type aggregateType, string type)
            : base(id, aggregateType)
        {
            Type = type;
        }
    }
}