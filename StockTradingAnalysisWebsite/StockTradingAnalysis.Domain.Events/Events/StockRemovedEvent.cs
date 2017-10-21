using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockRemovedEvent : DomainEvent
    {
        public StockRemovedEvent(Guid id, Type aggregateType)
            : base(id, aggregateType)
        {
        }
    }
}