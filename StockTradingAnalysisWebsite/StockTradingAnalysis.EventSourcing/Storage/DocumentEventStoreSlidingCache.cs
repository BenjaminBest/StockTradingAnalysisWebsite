using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// The DocumentEventStoreSlidingCache implements a sliding cache strategy based on aggregates
    /// </summary>
    /// <seealso cref="IDocumentEventStoreCache" />
    public class DocumentEventStoreSlidingCache : IDocumentEventStoreCache
    {
        /// <summary>
        /// The amount of aggregates to cache
        /// </summary>
        private readonly int _amountOfAggregatesToCache;

        /// <summary>
        /// The cache which used aggregateIds as keys
        /// </summary>
        private Dictionary<Guid, HashSet<IDomainEvent>> _cache = new Dictionary<Guid, HashSet<IDomainEvent>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentEventStoreSlidingCache"/> class.
        /// </summary>
        /// <param name="amountOfAggregatesToCache">The amount of aggregates to hold in the cache.</param>
        public DocumentEventStoreSlidingCache(int amountOfAggregatesToCache)
        {
            _amountOfAggregatesToCache = amountOfAggregatesToCache;
        }

        /// <summary>
        /// Adds the given <paramref name="event" /> to the internal cache
        /// </summary>
        /// <param name="event">The event</param>
        public void Add(IDomainEvent @event)
        {
            if (_cache.ContainsKey(@event.AggregateId))
            {
                _cache[@event.AggregateId].Add(@event);
                return;
            }

            if (_cache.Keys.Count >= _amountOfAggregatesToCache)
            {
                var keyRemoved = _cache.ElementAt(0).Key;
                _cache.Remove(keyRemoved);
            }

            _cache.Add(@event.AggregateId, new HashSet<IDomainEvent> { @event });
        }

        /// <summary>
        /// Adds the given <paramref name="events" /> to the internal cache
        /// </summary>
        /// <param name="events">A list of events</param>
        public void Add(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                Add(@event);
            }
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> Get(Guid aggregateId)
        {
            if (_cache.ContainsKey(aggregateId))
                return _cache[aggregateId].AsEnumerable();

            return Enumerable.Empty<IDomainEvent>();
        }
    }
}
