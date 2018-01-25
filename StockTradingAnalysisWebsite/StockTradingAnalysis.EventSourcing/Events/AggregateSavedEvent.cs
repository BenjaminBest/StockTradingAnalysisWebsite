using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.Events
{
    /// <summary>
    /// The aggregate saved event is fired by the event store, this is a pseudo event which is not persisted by the event store
    /// </summary>
    public class AggregateSavedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes this event
        /// </summary>
        /// <param name="aggregateId">The id of an aggregate</param>
        /// <param name="aggregateType">The type of an aggregate</param>
        /// <param name="version">The version of the aggregate</param>
        public AggregateSavedEvent(Guid aggregateId, Type aggregateType, int version)
            : base(aggregateId, aggregateType)
        {
            Version = version;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateSavedEvent"/> class.
        /// </summary>
        protected AggregateSavedEvent()
        {

        }
    }
}