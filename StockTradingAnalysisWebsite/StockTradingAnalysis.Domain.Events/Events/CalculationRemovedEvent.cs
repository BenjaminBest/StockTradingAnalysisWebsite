using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationRemovedEvent : DomainEvent
    {
        public CalculationRemovedEvent(Guid id, Type aggregateType)
            : base(id, aggregateType)
        {
        }
    }
}