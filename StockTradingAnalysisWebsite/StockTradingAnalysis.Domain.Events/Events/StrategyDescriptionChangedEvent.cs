using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StrategyDescriptionChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyDescriptionChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="description">The description.</param>
        public StrategyDescriptionChangedEvent(Guid id, Type aggregateType, string description)
            : base(id, aggregateType)
        {
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyDescriptionChangedEvent"/> class.
        /// </summary>
        protected StrategyDescriptionChangedEvent()
        {
            
        }
    }
}