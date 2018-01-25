using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Domain.Events.Domain;
using StockTradingAnalysis.Domain.Events.Snapshots;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Data.MSSQL.Tests
{
    [TestClass]
    public class SnapShotDatastoreTests
    {

        private static readonly StockAggregateSnapshot _testAggregate = new StockAggregateSnapshot(Guid.Parse("a750f2cc-2452-4bce-b720-e9bd647b936b"), 1, "Name", "WKN", "Type", "long", new HashSet<IQuotation>
        {
            new Quotation(DateTime.Parse("2018-01-23T17:06:42.418107+01:00"),
                DateTime.Parse("2018-01-23T17:06:42.418107+01:00"), 1, 2, 3, 4)
        });

        private static string _testAggregateSerialized = "{\"$type\":\"StockTradingAnalysis.Domain.Events.Snapshots.StockAggregateSnapshot, StockTradingAnalysis.Domain.Events\",\"Name\":\"Name\",\"Wkn\":\"WKN\",\"Type\":\"Type\",\"LongShort\":\"long\",\"Quotations\":{\"$type\":\"System.Collections.Generic.HashSet`1[[StockTradingAnalysis.Interfaces.Domain.IQuotation, StockTradingAnalysis.Interfaces]], System.Core\",\"$values\":[{\"$type\":\"StockTradingAnalysis.Domain.Events.Domain.Quotation, StockTradingAnalysis.Domain.Events\",\"Date\":\"2018-01-23T17:06:42.418107+01:00\",\"Changed\":\"2018-01-23T17:06:42.418107+01:00\",\"Open\":1.0,\"Close\":2.0,\"High\":3.0,\"Low\":4.0}]},\"Id\":\"9745ad72-cca6-4701-9090-11d829d9ad4c\",\"AggregateId\":\"a750f2cc-2452-4bce-b720-e9bd647b936b\",\"Version\":1}";

        [TestMethod]
        [Description("MSSQL SnapShotDatastore should serialize an aggregate in such a manor, that it can be correctly deserialized")]
        public void MssqlSnapShotDataStoreSerializeShouldCreateCorrectStringWhenCalledWithADomainEvent()
        {
            var serialized = SnapShotDatastore.Serialize(_testAggregate);
            serialized.Should().Contain("StockTradingAnalysis.Domain.Events.Snapshots.StockAggregateSnapshot, StockTradingAnalysis.Domain.Events");
            serialized.Should().Contain("Name");
            serialized.Should().Contain("WKN");
            serialized.Should().Contain("Type");
            serialized.Should().Contain("long");
            serialized.Should().Contain("StockTradingAnalysis.Interfaces.Domain.IQuotation");
            serialized.Should().Contain("a750f2cc-2452-4bce-b720-e9bd647b936b");
        }

        [TestMethod]
        [Description("MSSQL SnapShotDatastore should deserialize an aggregate with all properties correctly filled")]
        public void MssqlSnapShotDataStoreDeserializeShouldCreateCorrectDomainEventWhenCalledWithAValidString()
        {
            var deserialized = SnapShotDatastore.Deserialize<StockAggregateSnapshot>(_testAggregateSerialized);

            deserialized.AggregateId.Should().Be(Guid.Parse("a750f2cc-2452-4bce-b720-e9bd647b936b"));
            deserialized.Id.Should().Be(Guid.Parse("9745ad72-cca6-4701-9090-11d829d9ad4c"));
            deserialized.LongShort.Should().Be("long");
            deserialized.Name.Should().Be("Name");
            deserialized.Type.Should().Be("Type");
            deserialized.Wkn.Should().Be("WKN");
            deserialized.Version.Should().Be(1);
            deserialized.Quotations.First().Changed.Should().Be(DateTime.Parse("2018-01-23T17:06:42.418107+01:00"));
            deserialized.Quotations.First().Date.Should().Be(DateTime.Parse("2018-01-23T17:06:42.418107+01:00"));
            deserialized.Quotations.First().Open.Should().Be(1);
            deserialized.Quotations.First().Close.Should().Be(2);
            deserialized.Quotations.First().High.Should().Be(3);
            deserialized.Quotations.First().Low.Should().Be(4);
        }
    }
}
