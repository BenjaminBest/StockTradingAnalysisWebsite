using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StrategyRemovedEvent : DomainEvent
    {
        public StrategyRemovedEvent(Guid id, Type aggregateType)
            : base(id, aggregateType)
        {
        }
    }
}