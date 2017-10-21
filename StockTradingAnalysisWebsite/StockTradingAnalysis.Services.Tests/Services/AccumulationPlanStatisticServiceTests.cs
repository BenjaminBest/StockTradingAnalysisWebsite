using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.Core.Services;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Services.Tests.Services
{
    [TestClass]
    public class AccumulationPlanStatisticServiceTests
    {
        private static AccumulationPlanStatisticService Initialize()
        {
            var dateCalcService = new DateCalculationService();
            var iirService = new InterestRateCalculatorService();
            var dispatcher = new Mock<IQueryDispatcher>();
            dispatcher.Setup(s => s.Execute(It.IsAny<StockQuotationsByIdQuery>())).Returns(new List<IQuotation>());

            return new AccumulationPlanStatisticService(dateCalcService, iirService, dispatcher.Object);
        }

        private readonly SellingTransaction _sell = new SellingTransaction(Guid.NewGuid()) { PositionSize = 1000, Taxes = 50, OrderCosts = 50 };
        private readonly DividendTransaction _div = new DividendTransaction(Guid.NewGuid()) { PositionSize = 1000, Taxes = 50, OrderCosts = 50 };

        //TODO: Calculate

        [TestMethod]
        [Description("CalculateSumDividends should not throw if no transaction are available")]
        public void CalculateSumDividendsShouldNotThrowIfNoTransactionAreAvailable()
        {
            Action act = () => Initialize().CalculateSumDividends(null);
            act.ShouldNotThrow();

            act = () => Initialize().CalculateSumDividends(Enumerable.Empty<ITransaction>());
            act.ShouldNotThrow();
        }

        [TestMethod]
        [Description("CalculateSumDividends should not throw if no dividend transaction is available")]
        public void CalculateSumDividendsShouldNotThrowIfNoDividendsAreAvailable()
        {
            var transactions = new List<ITransaction> { _sell };

            Action act = () => Initialize().CalculateSumDividends(transactions);
            act.ShouldNotThrow();
        }

        [TestMethod]
        [Description("CalculateSumDividends should sum up a single dividend transaction")]
        public void CalculateSumDividendsShouldSumUpSingleDividendTransaction()
        {
            var transactions = new List<ITransaction> { _div };

            Initialize().CalculateSumDividends(transactions).Should().Be(900);
        }

        [TestMethod]
        [Description("CalculateSumDividends should sum up multiple dividend transactions")]
        public void CalculateSumDividendsShouldSumUpMultipleDividendTransactions()
        {
            var transactions = new List<ITransaction> { _div, _div };

            Initialize().CalculateSumDividends(transactions).Should().Be(1800);
        }

        [TestMethod]
        [Description("CalculateSumDividends should only sum up dividend transactions")]
        public void CalculateSumDividendsShouldOnlySumUpDividendTransactions()
        {
            var transactions = new List<ITransaction> { _div, _div, _sell };

            Initialize().CalculateSumDividends(transactions).Should().Be(1800);
        }

        //TODO: CalculateSumInpayment
        //TODO: CalculateSumCapital
        //TODO: CalculatePerformancePercentageIIR

        [TestMethod]
        [Description("Calculate should return null if no transaction are available")]
        public void CalculateShouldReturnNullIfNoTransactionAreAvailable()
        {
            Initialize().Calculate(null).Should().BeNull();
            Initialize().Calculate(Enumerable.Empty<ITransaction>()).Should().BeNull();
        }

        [TestMethod]
        [Description("CalculatePerformancePercentageGeometrical should not throw div by zero exception")]
        public void CalculatePerformancePercentageGeometricalShouldNotThrowDivByZeroException()
        {
            Action act = () => Initialize().CalculatePerformancePercentageGeometrical(0, 0);
            act.ShouldNotThrow();
        }

        [TestMethod]
        [Description("CalculatePerformancePercentageGeometrical should calculate positive interest rate")]
        public void CalculatePerformancePercentageGeometricalShouldCalculatePositiveInterestRate()
        {
            Initialize().CalculatePerformancePercentageGeometrical(10000, 12000).Should().Be(16.67m);
        }

        [TestMethod]
        [Description("CalculatePerformancePercentageGeometrical should calculate negative interest rate")]
        public void CalculatePerformancePercentageGeometricalShouldCalculateNegativeInterestRate()
        {
            Initialize().CalculatePerformancePercentageGeometrical(12000, 10000).Should().Be(-20.00m);
        }

        [TestMethod]
        [Description("CalculatePerformancePercentageGeometrical should calculate zero interest rate")]
        public void CalculatePerformancePercentageGeometricalShouldCalculateZeroInterestRate()
        {
            Initialize().CalculatePerformancePercentageGeometrical(12000, 12000).Should().Be(0m);
        }
    }
}
