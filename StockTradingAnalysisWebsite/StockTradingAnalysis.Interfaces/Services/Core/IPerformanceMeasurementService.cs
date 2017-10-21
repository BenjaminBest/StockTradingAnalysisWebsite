using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Interfaces.Services
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
    }
}