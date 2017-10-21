using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Linq;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    ///     Defines an an snapshot store which saves and returns the current state of originators (aggregates)
    /// </summary>
    public class SnapshotStore : ISnapshotStore
    {
        private readonly IPersistentSnapshotStore _persistentSnapshotStore;

        /// <summary>
        ///     Initializes this object
        /// </summary>
        /// <param name="persistentSnapshotStore">The persistent store (underlying data storage)</param>
        public SnapshotStore(IPersistentSnapshotStore persistentSnapshotStore)
        {
            _persistentSnapshotStore = persistentSnapshotStore;
        }

        /// <summary>
        ///     Saves the given snapshot <paramref name="originator" />
        /// </summary>
        /// <param name="originator">Snapshot of an aggregate</param>
        public void SaveSnapshot(ISnapshotOriginator originator)
        {
            var snapshot = originator.GetSnapshot();

            _persistentSnapshotStore.Add(snapshot);
        }

        /// <summary>
        ///     Returns the last snapshot for the given <paramref name="aggregateId" />
        /// </summary>
        /// <param name="aggregateId">The id of an aggregate</param>
        /// <returns>Snapshot</returns>
        public SnapshotBase GetSnapshot(Guid aggregateId)
        {
            return _persistentSnapshotStore.Find(aggregateId).FirstOrDefault();
        }
    }
}