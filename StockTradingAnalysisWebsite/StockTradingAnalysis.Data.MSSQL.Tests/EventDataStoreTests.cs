using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Data.MSSQL.Tests
{
    [TestClass]
    public class EventDataStoreTests
    {
        private static readonly TestEvent _testEvent = new TestEvent(Guid.Parse("B02386B2-0884-4143-9A1C-3D508C572BB4"),
            typeof(TestAggregate), "Name", "WKN", "Type", "long");

        private static string _testEventSerialized =
                "{\"$type\":\"StockTradingAnalysis.Data.MSSQL.Tests.EventDataStoreTests+TestEvent, StockTradingAnalysis.Data.MSSQL.Tests\",\"Name\":\"Name\",\"Wkn\":\"WKN\",\"Type\":\"Type\",\"LongShort\":\"long\",\"TimeStamp\":\"2018-01-23T17:06:42.418107+01:00\",\"AggregateId\":\"b02386b2-0884-4143-9a1c-3d508c572bb4\",\"AggregateType\":\"StockTradingAnalysis.Data.MSSQL.Tests.EventDataStoreTests+TestAggregate, StockTradingAnalysis.Data.MSSQL.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"Id\":\"be02c64a-2f9c-43b1-af21-1140f94f6ba4\",\"EventName\":\"TestEvent\",\"Version\":0}"
            ;

        [TestMethod]
        [Description("MSSQL Eventdatastore should serialize a domain event in such a manor, that it can be correctly deserialized")]
        public void MssqlEventDataStoreSerializeShouldCreateCorrectStringWhenCalledWithADomainEvent()
        {
            var serializedEvent = new EventDatastore("connection", "tableName", new Mock<IPerformanceMeasurementService>().Object).Serialize(_testEvent);
            serializedEvent.Should().Contain("StockTradingAnalysis.Data.MSSQL.Tests.EventDataStoreTests+TestEvent, StockTradingAnalysis.Data.MSSQL.Tests");
            serializedEvent.Should().Contain("Name");
            serializedEvent.Should().Contain("WKN");
            serializedEvent.Should().Contain("Type");
            serializedEvent.Should().Contain("long");
            serializedEvent.Should().Contain("TestEvent");
            serializedEvent.Should().Contain("0");
        }

        [TestMethod]
        [Description("MSSQL Eventdatastore should deserialize an aggregate with all properties correctly filled")]
        public void MssqlEventDataStoreDeserializeShouldCreateCorrectDomainEventWhenCalledWithAValidString()
        {
            var deserializedEvent = new EventDatastore("connection", "tableName", new Mock<IPerformanceMeasurementService>().Object).Deserialize<TestEvent>(_testEventSerialized);

            deserializedEvent.AggregateId.Should().Be(Guid.Parse("b02386b2-0884-4143-9a1c-3d508c572bb4"));
            deserializedEvent.Id.Should().Be(Guid.Parse("be02c64a-2f9c-43b1-af21-1140f94f6ba4"));
            deserializedEvent.TimeStamp.Should().Be(DateTime.Parse("2018-01-23T17:06:42.418107+01:00"));
            deserializedEvent.LongShort.Should().Be("long");
            deserializedEvent.Name.Should().Be("Name");
            deserializedEvent.Type.Should().Be("Type");
            deserializedEvent.Wkn.Should().Be("WKN");
            deserializedEvent.AggregateType.Should().Be(typeof(TestAggregate));
            deserializedEvent.EventName.Should().Be("TestEvent");
            deserializedEvent.Version.Should().Be(0);
        }

        private class TestAggregate
        {

        }

        private class TestEvent : DomainEvent
        {
            public string Name { get; private set; }

            public string Wkn { get; private set; }

            public string Type { get; private set; }

            public string LongShort { get; private set; }

            public TestEvent(Guid id, Type aggregateType, string name, string wkn, string type, string longShort)
                : base(id, aggregateType)
            {
                Name = name;
                Wkn = wkn;
                Type = type;
                LongShort = longShort;
            }

            protected TestEvent()
            {

            }
        }
    }
}
