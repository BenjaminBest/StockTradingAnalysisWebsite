using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class QuotationTests
    {
        [TestMethod]
        [Description("Quotations of stock should always be initialized")]
        public void StockQuotationsShouldAlwaysBeInitialized()
        {
            var stock = new Stock(Guid.NewGuid());

            stock.Quotations.Should().NotBeNull();
        }

        [TestMethod]
        [Description("Quotation should be added to stock quotations list")]
        public void StockQuotationShouldBeAdded()
        {
            var quotation = new Quotation(DateTime.Parse("2016-01-01 00:00:00"), DateTime.Now, 5, 10, 10, 5);

            var stock = new Stock(Guid.NewGuid());

            stock.AddQuotation(quotation);

            stock.Quotations.Should().NotBeNull();
            stock.Quotations.Should().HaveCount(1);
            stock.Quotations.FirstOrDefault().Open.Should().Be(5);
        }

        [TestMethod]
        [Description("If quotation for the date already exists value should be updated not inserted")]
        public void StockQuotationsShouldBeUpdatedIfQuotationAlreadyExists()
        {
            var quotation = new Quotation(DateTime.Parse("2016-01-01 00:00:00"), DateTime.Now, 5, 10, 10, 5);
            var quotationNew = new Quotation(DateTime.Parse("2016-01-01 00:00:00"), DateTime.Now, 55, 10, 10, 5);

            var stock = new Stock(Guid.NewGuid());

            stock.AddQuotation(quotation);
            stock.AddQuotation(quotationNew);

            stock.Quotations.Should().NotBeNull();
            stock.Quotations.Should().HaveCount(1);
            stock.Quotations.FirstOrDefault().Open.Should().Be(55);
        }

        [TestMethod]
        [Description("Quotations of stock aggregate should always be initialized")]
        public void StockAggrégateQuotationsShouldAlwaysBeInitialized()
        {
            var aggregate = new StockAggregate();
            var snapshot = aggregate.GetSnapshot() as StockAggregateSnapshot;

            snapshot.Quotations.Should().NotBeNull();
        }

        [TestMethod]
        [Description("Quotation should be added to stock aggregate quotations list")]
        public void StockAggregateQuotationShouldBeAdded()
        {
            var quotation = new Quotation(DateTime.Parse("2016-01-01 00:00:00"), DateTime.Now, 5, 10, 10, 5);

            var aggregate = new StockAggregate();

            aggregate.AddOrChangeQuotation(quotation);
            var snapshot = aggregate.GetSnapshot() as StockAggregateSnapshot;

            snapshot.Quotations.Should().NotBeNull();
            snapshot.Quotations.Should().HaveCount(1);
            snapshot.Quotations.FirstOrDefault().Open.Should().Be(5);
        }

        [TestMethod]
        [Description("If quotation for the date already exists value should be updated not inserted")]
        public void StockAggregateQuotationsShouldBeUpdatedIfQuotationAlreadyExists()
        {
            var quotation = new Quotation(DateTime.Parse("2016-01-01 00:00:00"), DateTime.Now, 5, 10, 10, 5);
            var quotationNew = new Quotation(DateTime.Parse("2016-01-01 00:00:00"), DateTime.Now, 55, 10, 10, 5);

            var aggregate = new StockAggregate();

            aggregate.AddOrChangeQuotation(quotation);
            aggregate.AddOrChangeQuotation(quotationNew);
            var snapshot = aggregate.GetSnapshot() as StockAggregateSnapshot;

            snapshot.Quotations.Should().NotBeNull();
            snapshot.Quotations.Should().HaveCount(1);
            snapshot.Quotations.FirstOrDefault().Open.Should().Be(55);
        }

        [TestMethod]
        [Description("Exactly the same quotations should have the same hashcode")]
        public void EqualQuotationsShouldHaveTheSameHashCode()
        {
            var quotation1 = new Quotation(DateTime.MinValue, DateTime.MinValue, 5, 5, 5, 5);
            var quotation2 = new Quotation(DateTime.MinValue, DateTime.MinValue, 5, 5, 5, 5);

            quotation1.GetHashCode().Should().Be(quotation2.GetHashCode());
        }

        [TestMethod]
        [Description("Exactly the same quotations should be equal")]
        public void ExactlyTheSameQuotationsShouldBeEqual()
        {
            var quotation1 = new Quotation(DateTime.MinValue, DateTime.MinValue, 5, 5, 5, 5);
            var quotation2 = new Quotation(DateTime.MinValue, DateTime.MinValue, 5, 5, 5, 5);

            quotation1.Equals(quotation2).Should().BeTrue();
        }

        [TestMethod]
        [Description("Quotations that differ should not be equal")]
        public void DifferentQuotationsShouldNotBeEqual()
        {
            var quotation1 = new Quotation(DateTime.MinValue, DateTime.MinValue, 5, 5, 5, 5);
            var quotation2 = new Quotation(DateTime.MinValue, DateTime.MinValue, 6, 5, 5, 5);

            quotation1.Equals(quotation2).Should().BeFalse();
        }

        [TestMethod]
        [Description("Equal quoations should be identifiable in a list")]
        public void EqualQuotationsShouldBeIdentifiableInList()
        {
            var quotation = new Quotation(DateTime.MinValue, DateTime.MinValue, 5, 5, 5, 5);

            var list = new List<Quotation> { quotation };

            list.Contains(quotation).Should().BeTrue();
        }

        [TestMethod]
        [Description("Unequal quoations should not be identifiable in a list")]
        public void UnequalQuotationsShouldNotBeIdentifiableInList()
        {
            var quotation1 = new Quotation(DateTime.MinValue, DateTime.MinValue, 5, 5, 5, 5);
            var quotation2 = new Quotation(DateTime.MinValue, DateTime.MinValue, 6, 5, 5, 5);

            var list = new List<Quotation> { quotation1 };

            list.Contains(quotation2).Should().BeFalse();
        }
    }


}