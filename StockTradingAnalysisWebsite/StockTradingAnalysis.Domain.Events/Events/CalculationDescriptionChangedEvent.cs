using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationDescriptionChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the description
        /// </summary>
        public string Description { get; private set; }

        public CalculationDescriptionChangedEvent(Guid id, Type aggregateType, string description)
            : base(id, aggregateType)
        {
            Description = description;
        }
    }
}