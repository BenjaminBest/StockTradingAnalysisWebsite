using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// DocumentDatabaseSnapshotStore uses as persistence layer a object oriented document database
    /// </summary>
    public class DocumentDatabaseSnapshotStore : IPersistentSnapshotStore
    {
        private readonly ISnapshotDatastore _datastore;

        /// <summary>
        /// Initializes this object
        /// </summary>
        public DocumentDatabaseSnapshotStore(ISnapshotDatastore datastore)
        {
            _datastore = datastore;
        }

        /// <summary>
        /// Adds the given <paramref name="snapshot" /> to the internal storage
        /// </summary>
        /// <param name="snapshot">Snapshot to be saved</param>
        public void Add(SnapshotBase snapshot)
        {
            _datastore.Store(snapshot);
        }

        /// <summary>
        /// Adds the given <paramref name="snapshots" /> to the internal storage
        /// </summary>
        /// <param name="snapshots">Snapshots to be saved</param>
        public void Add(IEnumerable<SnapshotBase> snapshots)
        {
            _datastore.Store(snapshots);

        }

        /// <summary>
        /// Returns all snapshots from the internal storage
        /// </summary>
        /// <returns>A list with all items</returns>
        public IEnumerable<SnapshotBase> All()
        {
            return _datastore.Select().OrderByDescending(s => s.Version);
        }

        /// <summary>
        /// Returns all snapshots which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<SnapshotBase> Find(Guid aggregateId)
        {
            return _datastore.Select(aggregateId).OrderByDescending(s => s.Version);
        }
    }
}