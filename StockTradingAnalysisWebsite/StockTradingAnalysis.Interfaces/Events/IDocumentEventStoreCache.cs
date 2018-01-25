using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The IDocumentEventStoreCache is a cache for the document store to improve performance
    /// </summary>
    public interface IDocumentEventStoreCache
    {
        /// <summary>
        /// Adds the given <paramref name="@event" /> to the internal cache
        /// </summary>
        /// <param name="event">The event</param>
        void Add(IDomainEvent @event);

        /// <summary>
        /// Adds the given <paramref name="@events" /> to the internal cache
        /// </summary>
        /// <param name="events">A list of events</param>
        void Add(IEnumerable<IDomainEvent> @events);

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all events</returns>
        IEnumerable<IDomainEvent> Get(Guid aggregateId);
    }
}