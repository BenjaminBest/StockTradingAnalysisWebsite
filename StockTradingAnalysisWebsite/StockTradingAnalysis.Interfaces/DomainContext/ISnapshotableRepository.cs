using System;

namespace StockTradingAnalysis.Interfaces.DomainContext
{
    /// <summary>
    /// Interface ISnapshotableRepository describes an aggregate repository which supports creating snapshots
    /// </summary>
    public interface ISnapshotableRepository
    {
        /// <summary>
        /// Returns the originator aka an aggregate which supports snapshots
        /// </summary>
        /// <param name="aggregateId">The Id of an aggregate</param>
        /// <returns>The snaphot originator</returns>
        ISnapshotOriginator GetOriginator(Guid aggregateId);

        /// <summary>
        /// Returns if underlying aggregate supports snapshots
        /// </summary>
        /// <returns><c>True</c>if snapshot is supported</returns>
        bool IsSnapshotSupported();

        /// <summary>
        /// Returns the type of the originator
        /// </summary>
        /// <returns>Type of the originator</returns>
        Type GetOriginatorType();
    }
}