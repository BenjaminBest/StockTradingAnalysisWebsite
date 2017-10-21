using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Web.Tests.Mocks;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class TimeMeasureTests
    {
        [TestMethod]
        [Description("Time measure should work in conjunction with performance service")]
        public void TimeMeasureShouldWorkInConjunctionWithPerformanceService()
        {
            var service = PerformanceCounterMock.GetMock();

            using (new TimeMeasure(ms => service.CountCommand(ms)))
            {
                Thread.Sleep(500);
            }

            var results = service.GetResults().ToDictionary(r => r.Key, r => r);

            results["Total Commands"].Value.Should().Be(1);
            results["Average Commands Duration"].Value.Should().BeApproximately(500, 5);
        }
    }
}