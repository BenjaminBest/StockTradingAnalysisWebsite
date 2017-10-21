using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Web.Tests.Mocks;
using System;
using System.Linq;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class TransactionBookTests
    {
        [TestMethod]
        [Description("Transaction book should not allow to under-sell units")]
        public void TransactionBookShouldOnlyAllowSellingWhenEnoughUnitsAvailable()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            Action act = () => book.AddEntry(TransactionEntryMock.CreateSelling(guid, 100));
            act.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        [Description("Transaction book should not return null value if position is empty")]
        public void TransactionBookShouldAlwaysReturnAPostion()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.GetOpenPosition(guid).Should().NotBeNull();
        }

        [TestMethod]
        [Description("Transaction book should return last changes")]
        public void TransactionBookShouldReturnLastChanges()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();
            var buy = TransactionEntryMock.CreateBuying(guid, 100);
            var sell = TransactionEntryMock.CreateSelling(guid, 100);

            book.AddEntry(buy);
            book.GetLastCommittedChanges(guid).Count().Should().Be(0);
            book.AddEntry(sell);
            book.GetLastCommittedChanges(guid).Count().Should().Be(2);
        }

        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for 100 Buy, 100 Sell")]
        public void TransactionBookShouldCalulateRemainingShares100Buy100Sell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 100));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 100));

            book.GetOpenPosition(guid).Shares.Should().Be(0);
        }

        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for 50 Buy, 50 Buy, 100 Sell")]
        public void TransactionBookShouldCalulateRemainingShares50Buy50Buy100Sell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 100));

            book.GetOpenPosition(guid).Shares.Should().Be(0);
        }

        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for  50 Buy, 50 Buy, 50 Sell, 50 Sell")]
        public void TransactionBookShouldCalulateRemainingShares50Buy50Buy50Sell50Sell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 50));

            book.GetOpenPosition(guid).Shares.Should().Be(0);
        }

        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for  100 Buy, 50 Sell, 50 Sell")]
        public void TransactionBookShouldCalulateRemainingShares100Buy50Sell50Sell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 100));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 50));

            book.GetOpenPosition(guid).Shares.Should().Be(0);
        }

        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for  100 Buy, 50 Sell")]
        public void TransactionBookShouldCalulateRemainingShares100Buy50Sell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 100));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 50));

            book.GetOpenPosition(guid).Shares.Should().Be(50);
        }

        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for  100 Buy, 50 Sell, 30 Sell")]
        public void TransactionBookShouldCalulateRemainingShares100Buy50Sell30Sell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 100));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 30));

            book.GetOpenPosition(guid).Shares.Should().Be(20);
        }

        [TestMethod]
        [Description("Transaction book should calculate the open position")]
        public void TransactionBookShouldCalulateOpenPositionWithoutSell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 20, 101.96m, 11.65m));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 30, 98m, 11.65m));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 33, 90.62m, 11.65m));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 35, 83m, 11.65m));

            var position = book.GetOpenPosition(guid);
            position.Shares.Should().Be(118);
            position.PricePerShare.Should().BeApproximately(92.553050m, 0.00001m);
            position.PositionSize.Should().Be(10921.26m);
            position.ProductId.Should().Be(guid);
        }

        [TestMethod]
        [Description("Transaction book should calculate the open position for a partial sale")]
        public void TransactionBookShouldCalulateOpenPositionWithOnePartialSale()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 20, 101.96m, 11.65m));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 30, 98m, 11.65m));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 33, 90.62m, 11.65m));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 35, 83m, 11.65m));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 60, 100m, 11.65m));

            var position = book.GetOpenPosition(guid);
            position.Shares.Should().Be(58);
            position.PricePerShare.Should().BeApproximately(0m, 0.00001m);//TODO:Get real values
            position.PositionSize.Should().Be(0m); //TODO:Get real values
            position.ProductId.Should().Be(guid);
        }

        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for 100 Buy, 100 Sell")]
        public void TransactionBookShouldCalulateRemainingShares100Buy100Dividend()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 100));
            book.AddEntry(TransactionEntryMock.CreateDividend(guid, 100));

            book.GetOpenPosition(guid).Shares.Should().Be(100);
        }

        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for  100 Buy, 50 Sell, 50 Dividend, 30 Sell")]
        public void TransactionBookShouldCalulateRemainingShares100Buy50Sell50Dividend30Sell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 100));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateDividend(guid, 50));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 30));

            book.GetOpenPosition(guid).Shares.Should().Be(20);
        }


        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for 1000 Buy, 500 Sell, 500 Split")]
        public void TransactionBookShouldCalulateRemainingShares1000Buy500Buy500Split()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 1000));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 500));
            book.AddEntry(TransactionEntryMock.CreateSplit(guid, 500));

            book.GetOpenPosition(guid).Shares.Should().Be(500);
        }


        [TestMethod]
        [Description("Transaction book should calculate the remaining shares for 1000 Buy, 500 Sell, 500 Split, 500 Sell")]
        public void TransactionBookShouldCalulateRemainingShares1000Buy500Buy500Split500Sell()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 1000));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 500));
            book.AddEntry(TransactionEntryMock.CreateSplit(guid, 500));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 500));

            book.GetOpenPosition(guid).Shares.Should().Be(0);
        }

        [TestMethod]
        [Description("Transaction book should calculate the average price using fifo")]
        public void TransactionBookShouldCalulateAveragePrizeUsingFifo()
        {
            var guid = Guid.NewGuid();
            var book = new TransactionBook();

            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 100, 1.2m, 0m));
            book.AddEntry(TransactionEntryMock.CreateBuying(guid, 50, 1.6m, 0m));
            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 80, 1.2m, 0m));

            book.GetOpenPosition(guid).PricePerShare.Should().BeApproximately(1.48571m, 0.00001m);

            book.AddEntry(TransactionEntryMock.CreateSelling(guid, 60, 1.46666m, 0m));

            book.GetOpenPosition(guid).PricePerShare.Should().BeApproximately(1.60m, 0.00001m);
        }
    }
}