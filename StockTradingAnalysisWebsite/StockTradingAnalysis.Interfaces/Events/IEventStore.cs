using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IEventStore defines the eventstore which saves incoming events and retrieves events
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// Returns all events order by the timestamp.
        /// </summary>
        /// <returns>A list with all events</returns>
        IEnumerable<IDomainEvent> GetEvents();

        /// <summary>
        /// Returns all events which aggregateId matches the given <paramref name="aggregateId"/>.
        /// </summary>
        /// <param name="aggregateId">The identifier of an aggregate</param>
        /// <returns>A list with all events</returns>
        IEnumerable<IDomainEvent> GetEventsByAggregateId(Guid aggregateId);

        /// <summary>
        /// Returns all events which aggregateId matches the given <paramref name="aggregateId"/> and the minimum version <paramref name="minVersion"/>
        /// </summary>
        /// <param name="aggregateId">The identifier of an aggregate</param>
        /// <param name="minVersion">A minimum version for the aggregate</param>
        /// <returns>A list with all events</returns>
        IEnumerable<IDomainEvent> GetEventsByAggregateId(Guid aggregateId, int minVersion);

        /// <summary>
        /// Saves the given <paramref name="events"/> to the store.
        /// </summary>
        /// <param name="events">A list of events</param>
        /// <param name="expectedVersion">The current version of the latest event for an aggregate</param>
        void Save(IEnumerable<IDomainEvent> events, int expectedVersion);
    }
}