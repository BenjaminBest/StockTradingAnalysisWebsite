using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Services;
using System.Linq;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class StatisticServiceTests
    {
        [TestMethod]
        [Description("Calculate should return null if no transaction are available")]
        public void CalculateShouldReturnNullIfNoTransactionAreAvailable()
        {
            new StatisticService().Calculate(null).Should().BeNull();
            new StatisticService().Calculate(Enumerable.Empty<ITransaction>()).Should().BeNull();
        }
    }
}
