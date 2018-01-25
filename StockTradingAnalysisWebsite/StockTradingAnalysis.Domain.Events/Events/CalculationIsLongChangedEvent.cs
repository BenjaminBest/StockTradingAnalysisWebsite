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

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationIsLongChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="isLong">if set to <c>true</c> [is long].</param>
        public CalculationIsLongChangedEvent(Guid id, Type aggregateType, bool isLong)
            : base(id, aggregateType)
        {
            IsLong = isLong;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationIsLongChangedEvent"/> class.
        /// </summary>
        protected CalculationIsLongChangedEvent()
        {
            
        }
    }
}