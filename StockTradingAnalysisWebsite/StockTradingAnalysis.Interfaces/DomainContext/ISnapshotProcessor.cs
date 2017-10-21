using System;

namespace StockTradingAnalysis.Interfaces.DomainContext
{
    /// <summary>
    /// The interface ISnapshotProcessor defines an snapshot-creator which actually creates and saves the snapshot
    /// </summary>
    public interface ISnapshotProcessor
    {
        /// <summary>
        /// Creates a new snapshot for the aggregate of type <paramref name="aggregateType"/> with id <paramref name="aggregateId"/>
        /// </summary>
        /// <param name="aggregateId">The Id of an aggregate</param>
        /// <param name="aggregateType">The type of an aggregate</param>
        void CreateSnapshot(Guid aggregateId, Type aggregateType);

        /// <summary>
        /// Returns <c>true</c> if a snapshot should be created
        /// </summary>
        /// <param name="aggregateId">The Id of an aggregate</param>
        /// <param name="aggregateType">The type of an aggregate</param>
        /// <param name="aggregateVersion">The current version of the aggregate</param>
        /// <returns><c>True</c> if a snapshot should be created</returns>
        bool IsSnapshotNeeded(Guid aggregateId, Type aggregateType, int aggregateVersion);
    }
}