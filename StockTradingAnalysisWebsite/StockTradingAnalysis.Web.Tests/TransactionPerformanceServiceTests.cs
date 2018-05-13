using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Services;
using StockTradingAnalysis.Web.Tests.Mocks;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class TransactionPerformanceServiceTests
    {
        private static ITransactionPerformance GetPerformance(ITransactionBook book, Guid stockId, Guid sellTransId)
        {
            var entries = book.GetLastCommittedChanges(stockId).ToList();
            var sell = entries.FirstOrDefault(e => e.TransactionId == sellTransId) as ISellingTransactionBookEntry;
            var other = entries.Where(e => e.TransactionId != sellTransId).Cast<IBuyingTransactionBookEntry>();
            return new TransactionPerformanceService().GetPerformance(sell, other, null, null);
        }

        private static ITransactionPerformance GetPerformance(ITransactionBook book, Guid stockId, Guid sellTransId, decimal? mfe, decimal? mae)
        {
            var entries = book.GetLastCommittedChanges(stockId).ToList();
            var sell = entries.FirstOrDefault(e => e.TransactionId == sellTransId) as ISellingTransactionBookEntry;
            var other = entries.Where(e => e.TransactionId != sellTransId).Cast<IBuyingTransactionBookEntry>();
            return new TransactionPerformanceService().GetPerformance(sell, other, mfe, mae);
        }

        private static ITransactionPerformance GetDividendPerformance(ITransactionBook book, Guid stockId, Guid sellTransId)
        {
            var entries = book.GetLastCommittedChanges(stockId).ToList();
            var dividend = entries.FirstOrDefault(e => e.TransactionId == sellTransId) as IDividendTransactionBookEntry;
            var other = entries.Where(e => e.TransactionId != sellTransId).Cast<IBuyingTransactionBookEntry>();
            return new TransactionPerformanceService().GetPerformance(dividend, other);
        }

        [TestMethod]
        [Description("Performance service should calculate holding period with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateHoldingPeriod1Buy1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 80, 6.69m, 11.65m, DateTime.Parse("2014-02-03 09:02:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 80, 5.99m, 11.65m, 0, DateTime.Parse("2014-04-15 11:28:00.000")));

            var result = GetPerformance(book, stockId, sellTransId);
            result.HoldingPeriod.IsIntradayTrade.Should().BeFalse();
            result.HoldingPeriod.ToDays().Should().Be(71.10m);
        }

        [TestMethod]
        [Description("Performance service should calculate holding period with 2 buys 1 sell")]
        public void PerformanceServiceShouldCalulateHoldingPeriodWith2Buys1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 370, 2.16m, 0, DateTime.Parse("2011-04-29 15:42:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 200, 2.30m, 0, DateTime.Parse("2011-05-04 10:13:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 570, 2.38m, 5.5m, 0, DateTime.Parse("2011-06-08 19:35:00.000")));

            var result = GetPerformance(book, stockId, sellTransId);
            result.HoldingPeriod.IsIntradayTrade.Should().BeFalse();
            result.HoldingPeriod.ToDays().Should().Be(40.16m);
        }

        [TestMethod]
        [Description("Performance service should calculate profit with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateProfitWith1Buy1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 80, 6.689m, 11.65m, DateTime.Parse("2014-02-03 09:02:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 80, 5.99m, 11.65m, 0, DateTime.Parse("2014-04-15 11:28:00.000")));

            var result = GetPerformance(book, stockId, sellTransId);
            result.ProfitMade.Should().BeFalse();
            result.ProfitAbsolute.Should().Be(-79.22m);
            result.ProfitPercentage.Should().Be(-14.49m);
        }

        [TestMethod]
        [Description("Performance service should calculate profit of a partly filled selling order")]
        public void PerformanceServiceShouldCalulateProfitOfPartlyFilledOrder()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 550, 1.92m, 9.90m, DateTime.Parse("2012-02-09 20:00:00.000")));
            //First sell
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 422, 1.75m, 11.15m, 0, DateTime.Parse("2012-03-01 09:02:00.000")));

            var result = GetPerformance(book, stockId, sellTransId);
            result.ProfitMade.Should().BeFalse();
            result.ProfitAbsolute.Should().Be(-90.49m);
            result.ProfitPercentage.Should().Be(-11.06m);

            //Second sell
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 128, 1.74m, 0.56m, 0, DateTime.Parse("2012-03-01 09:02:00.000")));
            result = GetPerformance(book, stockId, sellTransId);
            result.ProfitMade.Should().BeFalse();
            result.ProfitAbsolute.Should().Be(-25.90m);
            result.ProfitPercentage.Should().Be(-10.44m);
        }

        [TestMethod]
        [Description("Performance service should calculate profit with 2 buys 1 sell")]
        public void PerformanceServiceShouldCalulateProfitWith2Buys1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 370, 2.16m, 0, DateTime.Parse("2011-04-29 15:42:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 200, 2.30m, 0, DateTime.Parse("2011-05-04 10:13:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 570, 2.38m, 5.5m, 29.55m, DateTime.Parse("2011-06-08 19:35:00.000")));

            var result = GetPerformance(book, stockId, sellTransId);
            result.ProfitMade.Should().BeTrue();
            result.ProfitAbsolute.Should().Be(62.35m);
            result.ProfitPercentage.Should().Be(4.95m);
        }

        [TestMethod]
        [Description("Performance service should calculate the same profit if its one are more buys")]
        public void PerformanceServiceShouldCalulateTheSameWayWithOneSingleAndMultipleBuys()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            //2 Buys
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 370, 2.16m, 0, DateTime.Parse("2011-04-29 15:42:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 200, 2.30m, 0, DateTime.Parse("2011-05-04 10:13:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 570, 2.38m, 5.5m, 29.55m, DateTime.Parse("2011-06-08 19:35:00.000")));

            var result = GetPerformance(book, stockId, sellTransId);
            var profit = result.ProfitAbsolute;
            var profitPercentage = result.ProfitPercentage;


            var newBook = new TransactionBook();

            //Aggregated 1 buy
            newBook.AddEntry(TransactionEntryMock.CreateBuying(stockId, 570, 2.20912280701754m, 0, DateTime.Parse("2011-04-29 15:42:00.000")));
            newBook.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 570, 2.38m, 5.5m, 29.55m, DateTime.Parse("2011-06-08 19:35:00.000")));

            var newResult = GetPerformance(newBook, stockId, sellTransId);
            newResult.ProfitMade.Should().BeTrue();
            newResult.ProfitAbsolute.Should().Be(profit);
            newResult.ProfitPercentage.Should().Be(profitPercentage);
        }

        [TestMethod]
        [Description("Performance service should calculate the profit using the FIFO priciples")]
        public void PerformanceServiceShouldCalulateTheProfitUsingTheFifoPrinciples()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 1000, 25, 0, DateTime.Parse("2008-04-15 00:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 1000, 40, 0, DateTime.Parse("2009-01-03 00:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 500, 26, 0, DateTime.Parse("2010-03-07 00:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 500, 30, 0, DateTime.Parse("2011-04-07 00:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 2200, 45, 0m, 0m, DateTime.Parse("2011-04-28 00:00:00.000")));

            var result = GetPerformance(book, stockId, sellTransId);
            result.ProfitAbsolute.Should().Be(28800);
        }

        [TestMethod]
        [Description("Performance service should calculate the profit for a dividend")]
        public void PerformanceServiceShouldCalulateTheProfitForADividend()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 1000, 25, 0, DateTime.Parse("2008-04-15 00:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 1000, 40, 0, DateTime.Parse("2009-01-03 00:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 500, 26, 0, DateTime.Parse("2010-03-07 00:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 500, 30, 0, DateTime.Parse("2011-04-07 00:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateDividend(stockId, sellTransId, 2200, 45, 0m, 0m, DateTime.Parse("2011-04-28 00:00:00.000")));

            var result = GetDividendPerformance(book, stockId, sellTransId);
            result.ProfitAbsolute.Should().Be(99000);
        }

        [TestMethod]
        [Description("Performance service should calculate MAE Absolute with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateMaeAbsoluteWit12Buy1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 400, 2.97m, 0, DateTime.Parse("2011-04-29 13:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 400, 3.32m, 5.5m, 0, DateTime.Parse("2011-06-08 19:19:00.000")));

            var result = GetPerformance(book, stockId, sellTransId, 3.58m, 2.80m);
            result.MAEAbsolute.Should().Be(73.50m);
        }

        [TestMethod]
        [Description("Performance service should calculate MFE Absolute with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateMfeAbsoluteWith1Buy1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 400, 2.97m, 0, DateTime.Parse("2011-04-29 13:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 400, 3.32m, 5.5m, 0, DateTime.Parse("2011-06-08 19:19:00.000")));

            var result = GetPerformance(book, stockId, sellTransId, 3.58m, 2.80m);
            result.MFEAbsolute.Should().Be(238.50m);
        }

        [TestMethod]
        [Description("Performance service should calculate entry effiency with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateEntryEfficiencyWith1Buy1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 400, 2.97m, 0, DateTime.Parse("2011-04-29 13:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 400, 3.32m, 5.5m, 0, DateTime.Parse("2011-06-08 19:19:00.000")));

            var result = GetPerformance(book, stockId, sellTransId, 3.58m, 2.80m);
            result.EntryEfficiency.Should().Be(78.21m);
        }

        [TestMethod]
        [Description("Performance service should calculate exit effiency with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateExitEfficiencyWith1Buy1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 400, 2.97m, 0, DateTime.Parse("2011-04-29 13:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 400, 3.32m, 5.5m, 0, DateTime.Parse("2011-06-08 19:19:00.000")));

            var result = GetPerformance(book, stockId, sellTransId, 3.58m, 2.80m);
            result.ExitEfficiency.Should().Be(66.67m);
        }

        [TestMethod]
        [Description("Performance service should calculate risk with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateRiskWith2Buys1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 370, 2.16m, 0, DateTime.Parse("2011-04-29 15:42:00.000")));
            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 200, 2.30m, 0, DateTime.Parse("2011-05-04 10:13:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 570, 2.38m, 5.5m, 29.55m, DateTime.Parse("2011-06-08 19:35:00.000")));

            var result = GetPerformance(book, stockId, sellTransId);
            result.R.Should().Be(0.89m);
        }

        [TestMethod]
        [Description("Performance service should calculate risk with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateRiskWith1Buy1Sell()
        {
            var stockId = Guid.NewGuid();
            var sellTransId = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(stockId, 400, 2.97m, 0, DateTime.Parse("2011-04-29 13:00:00.000")));
            book.AddEntry(TransactionEntryMock.CreateSelling(stockId, sellTransId, 400, 3.32m, 5.5m, 10.71m, DateTime.Parse("2011-06-08 19:19:00.000")));

            var result = GetPerformance(book, stockId, sellTransId, 3.58m, 2.80m);
            result.R.Should().Be(1.77m);
        }

        [TestMethod]
        [Description("Performance service should calculate crv with 1 buy 1 sell")]
        public void PerformanceServiceShouldCalulateCrvWith1Buy1Sell()
        {
            var service = new TransactionPerformanceService();

            service.GetCRV(9.14m, 7.19m, 7.73m, 9.07m, 140).Should().Be(2.22m);
        }

        //TODO:Test Dividend
        //TODO:GetAverageBuyingPricePerUnit
    }
}