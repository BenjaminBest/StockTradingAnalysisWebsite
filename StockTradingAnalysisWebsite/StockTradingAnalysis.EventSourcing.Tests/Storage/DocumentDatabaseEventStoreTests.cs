using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.EventSourcing.Tests.Mocks;
using StockTradingAnalysis.Interfaces.Data;

namespace StockTradingAnalysis.EventSourcing.Tests.Storage
{
    [TestClass]
    public class DocumentDatabaseEventStoreTests
    {
        [TestMethod]
        [Description("The eventstore should get the domain events in the same order the persistent event store returns them")]
        public void DocumentDatabaseEventStoreAllShouldReturnAnTimestampOrderedListWhenCalled()
        {
            var eventDatastore = new Mock<IEventDatastore>();
            eventDatastore.Setup(s => s.Select()).Returns(DomainEventsMock.CreateUnorderedList);

            var documentDatabaseEventStore = new DocumentDatabaseEventStore(eventDatastore.Object);

            var results = documentDatabaseEventStore.All();

            results.ElementAt(0).EventName.Should().Be("1");
            results.ElementAt(0).TimeStamp.Should().Be(DateTime.Parse("2018-01-01 15:00:00"));
            results.ElementAt(1).EventName.Should().Be("2");
            results.ElementAt(1).Version.Should().Be(1);
            results.ElementAt(2).EventName.Should().Be("3");
            results.ElementAt(3).EventName.Should().Be("4");
            results.ElementAt(4).EventName.Should().Be("5");
            results.ElementAt(4).Id.Should().Be(Guid.Parse("7AB7CB93-ADF3-4E2E-88F9-6C9D36DFE440"));
        }

        [TestMethod]
        [Description("The eventstore should get the domain events in the same order the persistent event store returns them")]
        public void DocumentDatabaseEventStoreAllShouldReturnAnTimestampOrderedListWhenCalledInAForeachLoop()
        {
            var eventDatastore = new Mock<IEventDatastore>();
            eventDatastore.Setup(s => s.Select()).Returns(DomainEventsMock.CreateUnorderedList);

            var documentDatabaseEventStore = new DocumentDatabaseEventStore(eventDatastore.Object);

            var results = documentDatabaseEventStore.All();

            foreach (var result in results)
            {
                result.EventName.Should().Be("1");
                result.TimeStamp.Should().Be(DateTime.Parse("2018-01-01 15:00:00"));
                break;
            }
        }
    }
}
