using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Interfaces.Services.Core
{
    /// <summary>
    /// The interface IPerformanceMeasurementService defines a service to measure performance / throughput
    /// </summary>
    public interface IPerformanceMeasurementService
    {
        /// <summary>
        /// Resets all counters
        /// </summary>
        void Reset();

        /// <summary>
        /// Returns a list with all performance results
        /// </summary>
        /// <returns>Performance results</returns>
        IEnumerable<IPerformanceCounterItem> GetResults();

        /// <summary>
        /// Measures the performance for creating an events commit
        /// </summary>
        /// <param name="eventsCount">Number of events commited</param>
        /// <param name="elapsedMilliseconds">Elapsed time for commit</param>
        void CountCommit(int eventsCount, long elapsedMilliseconds);

        /// <summary>
        /// Measures the performance for reading data from the database 
        /// </summary>
        void CountDatabaseReads();

        /// <summary>
        /// Measures the performance for writing data to the database 
        /// </summary>
        void CountDatabaseWrites();

        /// <summary>
        /// Measures the performance for reading items in the documentdatabase eventstore
        /// </summary>
        /// <param name="eventsCount">Number of events commited</param>
        void CountDocumentDatabaseEventStoreRead(int eventsCount);

        /// <summary>
        /// Measures the performance for reading items in the documentdatabase snapshotstore
        /// </summary>
        /// <param name="eventsCount">Number of events commited</param>
        void CountDocumentDatabaseSnapshotStoreRead(int eventsCount);

        /// <summary>
        /// Measures the performance for creating a snapshot
        /// </summary>
        void CountSnapshot();

        /// <summary>
        /// Measures the performance for creating running a query
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed time for running a query</param>
        void CountQuery(long elapsedMilliseconds);

        /// <summary>
        /// Measures the performance for running a command
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed time for running a command</param>
        void CountCommand(long elapsedMilliseconds);

        /// <summary>
        /// Measures the performance of serialization
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed time for serialization</param>
        void CountSerializedObjects(long elapsedMilliseconds);

        /// <summary>
        /// Measures the performance of deserialization
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed time for deserialization</param>
        void CountDeserializedObjects(long elapsedMilliseconds);
    }
}