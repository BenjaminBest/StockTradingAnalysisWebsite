using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.EventSourcing.Tests.Mocks;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.EventSourcing.Tests.Storage
{
    [TestClass]
    public class EventStoreTests
    {
        [TestMethod]
        [Description("The eventstore should get the domain events in the same order the persistent event store returns them")]
        public void EventStoreGetEventsShouldBeSortedTheSameAsThePersistentEventStoreWhenCalled()
        {
            var eventBus = new Mock<IEventBus>();
            var persistentEventStore = new Mock<IPersistentEventStore>();
            var performance = new Mock<IPerformanceMeasurementService>();

            persistentEventStore.Setup(s => s.All()).Returns(DomainEventsMock.CreateUnorderedList());

            var eventStore = new EventStore(eventBus.Object, persistentEventStore.Object, performance.Object);

            var firstEvent = eventStore.GetEvents().First();
            var lastEvent = eventStore.GetEvents().Last();
            firstEvent.EventName.Should().Be("2");
            lastEvent.EventName.Should().Be("3");
        }
    }
}
