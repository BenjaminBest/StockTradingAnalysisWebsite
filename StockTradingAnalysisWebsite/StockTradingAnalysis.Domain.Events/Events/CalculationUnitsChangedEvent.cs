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

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationUnitsChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="units">The units.</param>
        public CalculationUnitsChangedEvent(Guid id, Type aggregateType, decimal units)
            : base(id, aggregateType)
        {
            Units = units;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationUnitsChangedEvent"/> class.
        /// </summary>
        protected CalculationUnitsChangedEvent()
        {

        }
    }
}