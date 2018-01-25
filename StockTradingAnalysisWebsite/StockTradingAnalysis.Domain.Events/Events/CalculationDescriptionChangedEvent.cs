using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationDescriptionChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationDescriptionChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="description">The description.</param>
        public CalculationDescriptionChangedEvent(Guid id, Type aggregateType, string description)
            : base(id, aggregateType)
        {
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationDescriptionChangedEvent"/> class.
        /// </summary>
        protected CalculationDescriptionChangedEvent()
        {

        }
    }
}