using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationIsLongChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets if its about selling or buying
        /// </summary>
        public bool IsLong { get; private set; }

        public CalculationIsLongChangedEvent(Guid id, Type aggregateType, bool isLong)
            : base(id, aggregateType)
        {
            IsLong = isLong;
        }
    }
}