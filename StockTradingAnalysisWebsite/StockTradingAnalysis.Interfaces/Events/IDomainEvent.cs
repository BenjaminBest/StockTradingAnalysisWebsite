using System;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// IDomainEvent defines an interface for an event
    /// </summary>
    public interface IDomainEvent : IMessage
    {
        /// <summary>
        /// Gets the time when this event was created
        /// </summary>
        DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the id of the aggregate this event is meant for
        /// </summary>
        Guid AggregateId { get; }

        /// <summary>
        /// Gets the type of the aggregate this event is meant for
        /// </summary>
        Type AggregateType { get; }

        /// <summary>
        /// Gets the name of this event
        /// </summary>
        string EventName { get; }

        /// <summary>
        /// Gets or sets the version of this event in relation to the aggregate
        /// </summary>
        int Version { get; set; }
    }
}