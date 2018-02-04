using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Services;

namespace StockTradingAnalysis.Services.Tests.Services
{
    [TestClass]
    public class TransactionCalculationServiceTests
    {
        private static ITransactionCalculationService Initialize(List<ITransaction> transactions = null)
        {
            var dateService = new Mock<IDateCalculationService>();
            var iirService = new Mock<IInterestRateCalculatorService>();
            var dispatcher = new Mock<IQueryDispatcher>();
            dispatcher.Setup(s => s.Execute(It.IsAny<IQuery<IEnumerable<ITransaction>>>())).Returns(transactions ?? new List<ITransaction>());

            return new TransactionCalculationService(dispatcher.Object, dateService.Object, iirService.Object);
        }

        private readonly SellingTransaction _sell = new SellingTransaction(Guid.NewGuid()) { PositionSize = 1000, Taxes = 50, OrderCosts = 50 };
        private readonly DividendTransaction _div = new DividendTransaction(Guid.NewGuid()) { PositionSize = 1000, Taxes = 50, OrderCosts = 50 };

        [TestMethod]
        [Description("CalculateSumDividends should not throw if no transaction are available")]
        public void CalculateSumDividendsShouldNotThrowIfNoTransactionAreAvailable()
        {
            Action act = () => Initialize().CalculateSumDividends(new TransactionAllQuery());
            act.ShouldNotThrow();

            act = () => Initialize().CalculateSumDividends(new TransactionAllQuery());
            act.ShouldNotThrow();
        }

        [TestMethod]
        [Description("CalculateSumDividends should not throw if no dividend transaction is available")]
        public void CalculateSumDividendsShouldNotThrowIfNoDividendsAreAvailable()
        {
            var transactions = new List<ITransaction> { _sell };

            Action act = () => Initialize(transactions).CalculateSumDividends(new TransactionAllQuery());
            act.ShouldNotThrow();
        }

        [TestMethod]
        [Description("CalculateSumDividends should sum up a single dividend transaction")]
        public void CalculateSumDividendsShouldSumUpSingleDividendTransaction()
        {
            var transactions = new List<ITransaction> { _div };

            Initialize(transactions).CalculateSumDividends(new TransactionAllQuery()).Should().Be(900);
        }

        [TestMethod]
        [Description("CalculateSumDividends should sum up multiple dividend transactions")]
        public void CalculateSumDividendsShouldSumUpMultipleDividendTransactions()
        {
            var transactions = new List<ITransaction> { _div, _div };

            Initialize(transactions).CalculateSumDividends(new TransactionAllQuery()).Should().Be(1800);
        }

        [TestMethod]
        [Description("CalculateSumDividends should only sum up dividend transactions")]
        public void CalculateSumDividendsShouldOnlySumUpDividendTransactions()
        {
            var transactions = new List<ITransaction> { _div, _div, _sell };

            Initialize(transactions).CalculateSumDividends(new TransactionAllQuery()).Should().Be(1800);
        }
    }
}
