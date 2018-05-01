using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Tests.Domain
{
    [TestClass]
    public class AllTimeSliceTests
    {
        [TestMethod]
        [Description("THe equals implementation of equatable should return true when start and end date are equal.")]
        public void AllTimeSliceEqualsShouldReturnTrueWhenStartAndEndDateAreEqual()
        {
            var first = new AllTimeSlice(DateTime.MinValue, DateTime.MaxValue);
            var second = new AllTimeSlice(DateTime.MinValue, DateTime.MaxValue);

            first.Equals(second).Should().BeTrue();
        }

        [TestMethod]
        [Description("THe equals implementation of equatable should return false when either start or end date are not equal.")]
        public void AllTimeSliceEqualsShouldReturnFalseWhenStartAndEndDateAreNotEqual()
        {
            var first = new AllTimeSlice(DateTime.MinValue, DateTime.MaxValue);
            var second = new AllTimeSlice(DateTime.MinValue, DateTime.Now);
            var third = new AllTimeSlice(DateTime.Now, DateTime.MaxValue);

            first.Equals(second).Should().BeFalse();
            first.Equals(third).Should().BeFalse();
        }

        [TestMethod]
        [Description("THe equals implementation of equatable should return true when a time slice is compared to a statistic with the same range.")]
        public void AllTimeSliceEqualsShouldReturnTrueWhenComparedToStatisticWithSameTimeRange()
        {
            var first = new AllTimeSlice(DateTime.MinValue, DateTime.MaxValue);
            var second = new Statistic(first);

            first.Equals(second).Should().BeTrue();
        }
    }
}
