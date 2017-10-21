using System;
using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// Defines an interface for an snapshot store which saves and returns the current state of originators (aggregates)
    /// </summary>
    public interface ISnapshotStore
    {
        /// <summary>
        /// Saves the given snapshot <paramref name="originator"/>
        /// </summary>
        /// <param name="originator">Snapshot of an aggregate</param>
        void SaveSnapshot(ISnapshotOriginator originator);

        /// <summary>
        /// Returns the last snapshot for the given <paramref name="aggregateId"/>
        /// </summary>
        /// <param name="aggregateId">The id of an aggregate</param>
        /// <returns>Snapshot</returns>
        SnapshotBase GetSnapshot(Guid aggregateId);
    }
}