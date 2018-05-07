using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Services.Services;

namespace StockTradingAnalysis.Services.Tests.Services
{
    [TestClass]
    public class StatisticServiceTests
    {
        [TestMethod]
        [Description("Calculate should return null if no transaction are available")]
        public void CalculateShouldReturnNullIfNoTransactionAreAvailable()
        {
            var dispatcher = new Mock<IQueryDispatcher>();
            dispatcher.Setup(s => s.Execute(It.IsAny<IQuery<IEnumerable<ITransaction>>>())).Returns(new List<ITransaction>());

            var mathService = new Mock<IMathCalculatorService>();

            new StatisticService(dispatcher.Object, mathService.Object).Calculate(null).Should().BeNull();
        }
    }
}
