using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class FeedbackAddedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the wkn
        /// </summary>
        public string Description { get; private set; }

        public FeedbackAddedEvent(Guid id, Type aggregateType, string name, string description)
            : base(id, aggregateType)
        {
            Name = name;
            Description = description;
        }
    }
}