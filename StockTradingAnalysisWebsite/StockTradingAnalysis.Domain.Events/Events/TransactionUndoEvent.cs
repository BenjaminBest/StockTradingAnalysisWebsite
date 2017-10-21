using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class TransactionUndoEvent : DomainEvent
    {
        public TransactionUndoEvent(Guid id, Type aggregateType)
            : base(id, aggregateType)
        {
        }
    }
}