using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class FeedbackRemovedEvent : DomainEvent
    {
        public FeedbackRemovedEvent(Guid id, Type aggregateType)
            : base(id, aggregateType)
        {
        }
    }
}