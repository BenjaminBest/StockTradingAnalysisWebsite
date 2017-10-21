using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationUnitsChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the amount of units
        /// </summary>
        public decimal Units { get; private set; }

        public CalculationUnitsChangedEvent(Guid id, Type aggregateType, decimal units)
            : base(id, aggregateType)
        {
            Units = units;
        }
    }
}