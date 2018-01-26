using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// The DocumentEventStoreSlidingCache implements a sliding cache strategy based on aggregates
    /// </summary>
    /// <seealso cref="Interfaces.Events.IDocumentStoreCache{IDomainEvent}" />
    public class DocumentSnapshotStoreSlidingCache : IDocumentStoreCache<SnapshotBase>
    {
        /// <summary>
        /// The amount of aggregates to cache
        /// </summary>
        private readonly int _amountOfAggregatesToCache;

        /// <summary>
        /// The cache which used aggregateIds as keys
        /// </summary>
        private readonly Dictionary<Guid, HashSet<SnapshotBase>> _cache = new Dictionary<Guid, HashSet<SnapshotBase>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Interfaces.Events.IDocumentStoreCache{TType}"/> class.
        /// </summary>
        /// <param name="amountOfAggregatesToCache">The amount of aggregates to hold in the cache.</param>
        public DocumentSnapshotStoreSlidingCache(int amountOfAggregatesToCache)
        {
            _amountOfAggregatesToCache = amountOfAggregatesToCache;
        }

        /// <summary>
        /// Adds the given <paramref name="item" /> to the internal cache
        /// </summary>
        /// <param name="item">The item</param>
        public void Add(SnapshotBase item)
        {
            if (_cache.ContainsKey(item.AggregateId))
            {
                _cache[item.AggregateId].Add(item);
                return;
            }

            if (_cache.Keys.Count >= _amountOfAggregatesToCache)
            {
                var keyRemoved = _cache.ElementAt(0).Key;
                _cache.Remove(keyRemoved);
            }

            _cache.Add(item.AggregateId, new HashSet<SnapshotBase> { item });
        }

        /// <summary>
        /// Adds the given <paramref name="items" /> to the internal cache
        /// </summary>
        /// <param name="items">A list of items</param>
        public void Add(IEnumerable<SnapshotBase> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all items</returns>
        public IEnumerable<SnapshotBase> Get(Guid aggregateId)
        {
            if (_cache.ContainsKey(aggregateId))
                return _cache[aggregateId].AsEnumerable();

            return Enumerable.Empty<SnapshotBase>();
        }
    }
}
