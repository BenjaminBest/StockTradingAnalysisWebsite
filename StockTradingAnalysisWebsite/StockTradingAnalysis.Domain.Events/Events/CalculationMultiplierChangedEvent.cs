using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationMultiplierChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the multiplier
        /// </summary>
        public decimal Multiplier { get; private set; }

        public CalculationMultiplierChangedEvent(Guid id, Type aggregateType, decimal multiplier)
            : base(id, aggregateType)
        {
            Multiplier = multiplier;
        }
    }
}