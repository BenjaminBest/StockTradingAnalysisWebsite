using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StrategyDescriptionChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description { get; private set; }

        public StrategyDescriptionChangedEvent(Guid id, Type aggregateType, string description)
            : base(id, aggregateType)
        {
            Description = description;
        }
    }
}