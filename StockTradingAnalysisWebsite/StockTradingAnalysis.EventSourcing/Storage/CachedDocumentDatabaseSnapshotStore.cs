using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// CachedDocumentDatabaseSnapshotStore uses as persistence layer a object oriented document database
    /// </summary>
    public class CachedDocumentDatabaseSnapshotStore : IPersistentSnapshotStore
    {
        /// <summary>
        /// The datastore
        /// </summary>
        private readonly ISnapshotDatastore _datastore;

        /// <summary>
        /// The document event store cache
        /// </summary>
        private readonly IDocumentStoreCache<SnapshotBase> _documentStoreCache;

        /// <summary>
        /// The performance measurement service
        /// </summary>
        private readonly IPerformanceMeasurementService _performanceMeasurementService;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="datastore">The datastore.</param>
        /// <param name="documentStoreCache">The document store cache.</param>
        /// <param name="performanceMeasurementService">The performance measurement service.</param>
        public CachedDocumentDatabaseSnapshotStore(ISnapshotDatastore datastore,
            IDocumentStoreCache<SnapshotBase> documentStoreCache,
            IPerformanceMeasurementService performanceMeasurementService)
        {
            _datastore = datastore;
            _documentStoreCache = documentStoreCache;
            _performanceMeasurementService = performanceMeasurementService;
        }

        /// <summary>
        /// Adds the given <paramref name="snapshot" /> to the internal storage
        /// </summary>
        /// <param name="snapshot">Snapshot to be saved</param>
        public void Add(SnapshotBase snapshot)
        {
            _documentStoreCache.Add(snapshot);
            _datastore.Store(snapshot);
        }

        /// <summary>
        /// Adds the given <paramref name="snapshots" /> to the internal storage
        /// </summary>
        /// <param name="snapshots">Snapshots to be saved</param>
        public void Add(IEnumerable<SnapshotBase> snapshots)
        {
            var snapshotBases = snapshots.ToList();

            _documentStoreCache.Add(snapshotBases);
            _datastore.Store(snapshotBases);

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
            List<SnapshotBase> snapshots = null;
            using (new TimeMeasure(ms => _performanceMeasurementService.CountDocumentDatabaseSnapshotStoreRead(snapshots.Count)))
            {
                snapshots = _documentStoreCache.Get(aggregateId).ToList();

                if (!snapshots.Any())
                    snapshots = _datastore.Select(aggregateId).ToList();

                return snapshots.OrderByDescending(s => s.Version);
            }
        }
    }
}