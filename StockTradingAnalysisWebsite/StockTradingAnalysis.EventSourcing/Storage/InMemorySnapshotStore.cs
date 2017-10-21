using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// Defines an in memory storage for mementos/snapshots
    /// </summary>
    public class InMemorySnapshotStore : IPersistentSnapshotStore
    {
        private readonly IList<SnapshotBase> _snapshots = new List<SnapshotBase>();

        /// <summary>
        /// Adds the given <paramref name="snapshot" /> to the internal storage
        /// </summary>
        /// <param name="snapshot">Snapshot to be saved</param>
        public void Add(SnapshotBase snapshot)
        {
            _snapshots.Add(snapshot);
        }

        /// <summary>
        ///     Adds the given <paramref name="snapshots" /> to the internal storage
        /// </summary>
        /// <param name="snapshots"></param>
        public void Add(IEnumerable<SnapshotBase> snapshots)
        {
            foreach (var item in snapshots)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Returns all snapshots from the internal storage
        /// </summary>
        /// <returns>A list with all items</returns>
        public IEnumerable<SnapshotBase> All()
        {
            return _snapshots.OrderByDescending(s => s.Version);
        }

        /// <summary>
        /// Returns all snapshots which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<SnapshotBase> Find(Guid aggregateId)
        {
            return _snapshots.Where(e => e.AggregateId.Equals(aggregateId)).OrderByDescending(s => s.Version);
        }
    }
}