using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class FeedbackAddedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the wkn
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackAddedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public FeedbackAddedEvent(Guid id, Type aggregateType, string name, string description)
            : base(id, aggregateType)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackAddedEvent"/> class.
        /// </summary>
        protected FeedbackAddedEvent()
        {

        }
    }
}