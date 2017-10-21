using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationUnderlyingChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the underlying
        /// </summary>
        public string Underlying { get; private set; }

        public CalculationUnderlyingChangedEvent(Guid id, Type aggregateType, string underlying)
            : base(id, aggregateType)
        {
            Underlying = underlying;
        }
    }
}