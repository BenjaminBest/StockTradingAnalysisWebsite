using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationRemovedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        public CalculationRemovedEvent(Guid id, Type aggregateType)
            : base(id, aggregateType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationRemovedEvent"/> class.
        /// </summary>
        protected CalculationRemovedEvent()
        {

        }
    }
}