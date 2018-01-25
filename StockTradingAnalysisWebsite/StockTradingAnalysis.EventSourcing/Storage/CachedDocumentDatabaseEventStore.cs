using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// DocumentDatabaseEventStore uses as persistence layer a object oriented document database
    /// </summary>
    public class CachedDocumentDatabaseEventStore : IPersistentEventStore
    {
        /// <summary>
        /// The persistent event store
        /// </summary>
        private readonly IPersistentEventStore _persistentEventStore;

        /// <summary>
        /// The document event store cache
        /// </summary>
        private readonly IDocumentEventStoreCache _documentEventStoreCache;

        /// <summary>
        /// The performance measurement service
        /// </summary>
        private readonly IPerformanceMeasurementService _performanceMeasurementService;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="persistentEventStore">The persistent event store.</param>
        /// <param name="documentEventStoreCache">The caching strategy used</param>
        /// <param name="_performanceMeasurementService">The performance measurement service.</param>
        public CachedDocumentDatabaseEventStore(
            IPersistentEventStore persistentEventStore,
            IDocumentEventStoreCache documentEventStoreCache,
            IPerformanceMeasurementService _performanceMeasurementService)
        {
            _persistentEventStore = persistentEventStore;
            _documentEventStoreCache = documentEventStoreCache;
            this._performanceMeasurementService = _performanceMeasurementService;
        }

        /// <summary>
        /// Adds the given <paramref name="@event" /> to the internal storage
        /// </summary>
        /// <param name="event">The event</param>
        public void Add(IDomainEvent @event)
        {
            _documentEventStoreCache.Add(@event);
            _persistentEventStore.Add(@event);
        }

        /// <summary>
        /// Adds the given <paramref name="@events" /> to the internal storage
        /// </summary>
        /// <param name="events">A list of events</param>
        public void Add(IEnumerable<IDomainEvent> events)
        {
            var domainEvents = events.ToList();

            _documentEventStoreCache.Add(domainEvents);
            _persistentEventStore.Add(domainEvents);
        }

        /// <summary>
        /// Returns all events from the internal storage
        /// </summary>
        /// <returns>A list with all items ordered by TimeStamp</returns>
        public IEnumerable<IDomainEvent> All()
        {
            return _persistentEventStore.All();
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> Find(Guid aggregateId)
        {
            List<IDomainEvent> events = null;
            using (new TimeMeasure(ms => _performanceMeasurementService.CountDocumentDatabaseEventStoreRead(events.Count, ms)))
            {
                events = _documentEventStoreCache.Get(aggregateId).ToList();

                if (!events.Any())
                    events = _persistentEventStore.Find(aggregateId).ToList();

                return events;
            }
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <param name="minVersion">The minimum version of the aggregate</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> Find(Guid aggregateId, int minVersion)
        {
            List<IDomainEvent> events = null;
            using (new TimeMeasure(ms => _performanceMeasurementService.CountDocumentDatabaseEventStoreRead(events.Count, ms)))
            {
                events = _documentEventStoreCache.Get(aggregateId).ToList();

                if (!events.Any())
                    events = _persistentEventStore.Find(aggregateId).ToList();

                return events.Where(e => e.Version > minVersion);
            }
        }
    }
}