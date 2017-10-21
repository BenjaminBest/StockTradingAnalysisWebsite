using System;

namespace StockTradingAnalysis.Interfaces.DomainContext
{
    /// <summary>
    /// The interface ISnapshotOriginator defines an interface to mark an aggregate which can create/apply snapshots
    /// </summary>
    public interface ISnapshotOriginator
    {
        /// <summary>
        /// Returns a snapshot of the instance
        /// </summary>
        /// <returns>Snapshot</returns>
        SnapshotBase GetSnapshot();

        /// <summary>
        /// Applies the given <paramref name="snapshot"/> to this instance
        /// </summary>
        /// <param name="snapshot">The saved state</param>
        void SetSnapshot(SnapshotBase snapshot);

        /// <summary>
        /// Gets the id of the originator
        /// </summary>
        Guid OriginatorId { get; }
    }
}
