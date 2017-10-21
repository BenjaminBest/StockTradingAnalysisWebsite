using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class FeedbackDescriptionChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description { get; private set; }

        public FeedbackDescriptionChangedEvent(Guid id, Type aggregateType, string description)
            : base(id, aggregateType)
        {
            Description = description;
        }
    }
}