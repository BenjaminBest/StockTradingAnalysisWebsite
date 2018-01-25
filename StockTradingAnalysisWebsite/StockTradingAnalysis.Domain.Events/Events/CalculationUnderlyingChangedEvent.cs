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

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationUnderlyingChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="underlying">The underlying.</param>
        public CalculationUnderlyingChangedEvent(Guid id, Type aggregateType, string underlying)
            : base(id, aggregateType)
        {
            Underlying = underlying;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationUnderlyingChangedEvent"/> class.
        /// </summary>
        protected CalculationUnderlyingChangedEvent()
        {

        }
    }
}