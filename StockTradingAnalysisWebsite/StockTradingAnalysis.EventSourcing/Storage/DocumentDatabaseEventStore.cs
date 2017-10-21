using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// DocumentDatabaseEventStore uses as persistence layer a object oriented document database
    /// </summary>
    public class DocumentDatabaseEventStore : IPersistentEventStore
    {
        private readonly IEventDatastore _datastore;

        /// <summary>
        /// Initializes this object
        /// </summary>
        public DocumentDatabaseEventStore(IEventDatastore datastore)
        {
            _datastore = datastore;
        }

        /// <summary>
        /// Adds the given <paramref name="@event" /> to the internal storage
        /// </summary>
        /// <param name="event">The event</param>
        public void Add(IDomainEvent @event)
        {
            _datastore.Store(@event);
        }

        /// <summary>
        /// Adds the given <paramref name="@events" /> to the internal storage
        /// </summary>
        /// <param name="events">A list of events</param>
        public void Add(IEnumerable<IDomainEvent> @events)
        {
            _datastore.Store(@events);
        }

        /// <summary>
        /// Returns all events from the internal storage
        /// </summary>
        /// <returns>A list with all items ordered by TimeStamp</returns>
        public IEnumerable<IDomainEvent> All()
        {
            return _datastore.Select().OrderBy(e => e.TimeStamp);
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> Find(Guid aggregateId)
        {
            return _datastore.Select(aggregateId).OrderBy(e => e.TimeStamp);
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <param name="minVersion">The minimum version of the aggregate</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> Find(Guid aggregateId, int minVersion)
        {
            return _datastore.Select(aggregateId, minVersion).OrderBy(e => e.TimeStamp);
        }
    }
}