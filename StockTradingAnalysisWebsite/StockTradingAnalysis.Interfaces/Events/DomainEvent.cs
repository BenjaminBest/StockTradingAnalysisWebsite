using System;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The DomainEvent is a base class for all events which contains all necessary
    /// </summary>
    /// <seealso cref="IDomainEvent" />
    public class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Gets the time when this event was created
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Gets the id of the aggregate this event is meant for
        /// </summary>
        public Guid AggregateId { get; set; }

        /// <summary>
        /// Gets the type of the aggregate this event is meant for
        /// </summary>
        public Type AggregateType { get; private set; }

        /// <summary>
        /// Gets the id of this event
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name of this event
        /// </summary>
        public string EventName { get; private set; }

        /// <summary>
        /// Gets or sets the version of this event in relation to the aggregate
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="aggregateId">The id of an aggregate</param>
        /// <param name="aggregateType">The type of the aggregate</param>
        public DomainEvent(Guid aggregateId, Type aggregateType)
        {
            AggregateId = aggregateId;
            AggregateType = aggregateType;

            TimeStamp = DateTime.Now;
            EventName = GetType().Name;
            Id = Guid.NewGuid();
            Version = -1;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DomainEvent"/> class from being created. This ctor is used by JSON serialization.
        /// </summary>
        protected DomainEvent()
        {

        }
    }
}