using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Performance;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Enumerations;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.Core.Services
{
    /// <summary>
    /// The class PerformanceMeasurementService defines a service to measure performance / throughput
    /// </summary>
    public class PerformanceMeasurementService : IPerformanceMeasurementService
    {
        private readonly IPerformanceRegistry _performanceRegistry;

        private readonly Dictionary<string, IPerformanceCounter> _counters = new Dictionary<string, IPerformanceCounter>();

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="performanceRegistry">The performance counter registry</param>
        public PerformanceMeasurementService(IPerformanceRegistry performanceRegistry)
        {
            _performanceRegistry = performanceRegistry;

            Initialize();
        }

        /// <summary>
        /// Initializes all performance counters
        /// </summary>
        private void Initialize()
        {
            Initialize("Total Commits", PerformanceType.NumberOfItems);
            Initialize("Total Events", PerformanceType.NumberOfItems);
            Initialize("Total Snapshots", PerformanceType.NumberOfItems);
            Initialize("Snapshots/ms", PerformanceType.RateOfCountsPerMillisecond);
            Initialize("Events/ms", PerformanceType.RateOfCountsPerMillisecond);
            Initialize("Commits/ms", PerformanceType.RateOfCountsPerMillisecond);
            Initialize("Average Commit Duration (ms)", PerformanceType.AverageTimer);
            Initialize("Total Queries", PerformanceType.NumberOfItems);
            Initialize("Queries/ms", PerformanceType.RateOfCountsPerMillisecond);
            Initialize("Average Queries Duration (ms)", PerformanceType.AverageTimer);
            Initialize("Total Commands", PerformanceType.NumberOfItems);
            Initialize("Commands/ms", PerformanceType.RateOfCountsPerMillisecond);
            Initialize("Average Commands Duration (ms)", PerformanceType.AverageTimer);
            Initialize("Events/ms read [DocumentDatabaseEventStore]", PerformanceType.RateOfCountsPerMillisecond);
            Initialize("Total Events read [DocumentDatabaseEventStore]", PerformanceType.NumberOfItems);
            Initialize("Total Database reads", PerformanceType.NumberOfItems);
            Initialize("Total Database writes", PerformanceType.NumberOfItems);

            //TODO: Maybe use const string
            //TODO: Possible counters (absolute time for dehydration)
            //	* Total Commit Bytes
            //  * Average Commit Bytes
            //  * Commits per Query (Total / average / per second)
            //  * Events per Query (Total / average / per second)
        }

        /// <summary>
        /// Creates and initializes a new specific performance counter of type <paramref name="type"/> with
        /// with given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the performance counter</param>
        /// <param name="type">The type of the performance counter</param>
        /// <returns></returns>
        private void Initialize(string name, PerformanceType type)
        {
            if (!_counters.ContainsKey(name))
                _counters.Add(name, _performanceRegistry.Create(type));
        }

        /// <summary>
        /// Resets all counters
        /// </summary>
        public void Reset()
        {
            foreach (var counter in _counters)
            {
                counter.Value.Reset();
            }
        }

        /// <summary>
        /// Returns a list with all performance results
        /// </summary>
        /// <returns>Performance results</returns>
        public IEnumerable<IPerformanceCounterItem> GetResults()
        {
            return _counters.Select(p => new PerformanceCounterItem(p.Key, p.Value.GetValue()));
        }

        /// <summary>
        /// Measures the performance for creating an events commit
        /// </summary>
        /// <param name="eventsCount">Number of events commited</param>
        /// <param name="elapsedMilliseconds">Elapsed time for commit</param>
        public void CountCommit(int eventsCount, long elapsedMilliseconds)
        {
            _counters["Total Commits"].Increment();
            _counters["Total Events"].IncrementBy(eventsCount);
            _counters["Commits/ms"].Increment();
            _counters["Events/ms"].IncrementBy(eventsCount);
            _counters["Average Commit Duration (ms)"].IncrementBy(elapsedMilliseconds);
        }

        /// <summary>
        /// Measures the performance for reading items in the documentdatabase eventstore
        /// </summary>
        /// <param name="eventsCount">Number of events commited</param>
        /// <param name="elapsedMilliseconds">Elapsed time for commit</param>
        public void CountDocumentDatabaseEventStoreRead(int eventsCount, long elapsedMilliseconds)
        {
            _counters["Events/ms read [DocumentDatabaseEventStore]"].Increment();
            _counters["Total Events read [DocumentDatabaseEventStore]"].IncrementBy(eventsCount);
        }

        /// <summary>
        /// Measures the performance for reading data from the database 
        /// </summary>
        public void CountDatabaseReads()
        {
            _counters["Total Database reads"].Increment();
        }

        /// <summary>
        /// Measures the performance for writing data to the database 
        /// </summary>
        public void CountDatabaseWrites()
        {
            _counters["Total Database writes"].Increment();
        }

        /// <summary>
        /// Measures the performance for creating a snapshot
        /// </summary>
        public void CountSnapshot()
        {
            _counters["Total Snapshots"].Increment();
            _counters["Snapshots/ms"].Increment();
        }

        /// <summary>
        /// Measures the performance for creating running a query
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed time for running a query</param>
        public void CountQuery(long elapsedMilliseconds)
        {
            _counters["Total Queries"].Increment();
            _counters["Queries/ms"].Increment();
            _counters["Average Queries Duration (ms)"].IncrementBy(elapsedMilliseconds);
        }

        /// <summary>
        /// Measures the performance for running a command
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed time for running a command</param>
        public void CountCommand(long elapsedMilliseconds)
        {
            _counters["Total Commands"].Increment();
            _counters["Commands/ms"].Increment();
            _counters["Average Commands Duration (ms)"].IncrementBy(elapsedMilliseconds);
        }

        //TODO: ToString to write all to console
        //TODO: Persistence
    }
}