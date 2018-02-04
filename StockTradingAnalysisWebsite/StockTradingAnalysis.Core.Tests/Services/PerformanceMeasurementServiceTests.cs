using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Core.Tests.Mocks;

namespace StockTradingAnalysis.Core.Tests.Services
{
    [TestClass]
    public class PerformanceMeasurementServiceTests
    {
        [TestMethod]
        [Description("PerformanceMeasurement should not throw an exception if no results are available")]
        public void PerformanceMeasurementShouldNotThrowIfNoResultsAreAvailable()
        {
            var service = PerformanceCounterMock.GetMock();

            Action act = () => service.GetResults();
            act.ShouldNotThrow();
        }

        [TestMethod]
        [Description("PerformanceMeasurement should calculate commit counters correctly")]
        public void PerformanceMeasurementShouldCalculateCommitCountersCorrectly()
        {
            var service = PerformanceCounterMock.GetMock();

            service.CountCommit(2, Convert.ToInt64(new TimeSpan(0, 0, 0, 0, 1000).TotalMilliseconds));
            service.CountCommit(1, Convert.ToInt64(new TimeSpan(0, 0, 0, 0, 1000).TotalMilliseconds));

            var results = service.GetResults().ToDictionary(r => r.Key, r => r);

            results["Total Commits"].Value.Should().Be(2);
            results["Total Events"].Value.Should().Be(3);
            results["Commits/ms"].Value.Should().BeApproximately(0.6d, 2);
            results["Events/ms"].Value.Should().BeApproximately(1, 0.5);
            results["Average Commit Duration (ms)"].Value.Should().Be(1000);
        }

        [TestMethod]
        [Description("PerformanceMeasurement should calculate snapshot counters correctly")]
        public void PerformanceMeasurementShouldCalculateSnapshotCountersCorrectly()
        {
            var service = PerformanceCounterMock.GetMock();

            service.CountSnapshot();
            service.CountSnapshot();

            var results = service.GetResults().ToDictionary(r => r.Key, r => r);

            results["Total Snapshots"].Value.Should().Be(2);
        }

        [TestMethod]
        [Description("PerformanceMeasurement should calculate query counters correctly")]
        public void PerformanceMeasurementShouldCalculateQueryCountersCorrectly()
        {
            var service = PerformanceCounterMock.GetMock();

            service.CountQuery(Convert.ToInt64(new TimeSpan(0, 0, 0, 0, 1000).TotalMilliseconds));
            service.CountQuery(Convert.ToInt64(new TimeSpan(0, 0, 0, 0, 1000).TotalMilliseconds));

            var results = service.GetResults().ToDictionary(r => r.Key, r => r);

            results["Total Queries"].Value.Should().Be(2);
            results["Average Queries Duration (ms)"].Value.Should().Be(1000);
        }

        [TestMethod]
        [Description("PerformanceMeasurement should calculate command counters correctly")]
        public void PerformanceMeasurementShouldCalculateCommandCountersCorrectly()
        {
            var service = PerformanceCounterMock.GetMock();

            service.CountCommand(Convert.ToInt64(new TimeSpan(0, 0, 0, 0, 1000).TotalMilliseconds));
            service.CountCommand(Convert.ToInt64(new TimeSpan(0, 0, 0, 0, 1000).TotalMilliseconds));

            var results = service.GetResults().ToDictionary(r => r.Key, r => r);

            results["Total Commands"].Value.Should().Be(2);
            results["Average Commands Duration (ms)"].Value.Should().Be(1000);
        }

        [TestMethod]
        [Description("PerformanceMeasurement should be working in a multithreaded environment")]
        public void PerformanceMeasurementShouldBeWorkingMultiThreaded()
        {
            var service = PerformanceCounterMock.GetMock();

            var thread1 = new Thread(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    service.CountCommand(Convert.ToInt64(new TimeSpan(0, 0, 0, 0, 1000).TotalMilliseconds));
                }
            });

            var thread2 = new Thread(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    service.CountCommand(Convert.ToInt64(new TimeSpan(0, 0, 0, 0, 1000).TotalMilliseconds));
                }
            });

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            var results = service.GetResults().ToDictionary(r => r.Key, r => r);

            results["Total Commands"].Value.Should().Be(200);
            results["Average Commands Duration (ms)"].Value.Should().Be(1000);
        }

    }
}