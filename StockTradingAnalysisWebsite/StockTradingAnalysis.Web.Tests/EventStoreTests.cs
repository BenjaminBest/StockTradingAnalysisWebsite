using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class EventStoreTests
    {
        [TestMethod]
        [Description("The eventstore should save every event that comes to the internal storage")]
        public void EventStoreShouldSaveEventToInternalStorage()
        {
            var handlers = new List<DependecyDescriptor>
            {
                new DependecyDescriptor(typeof (IEventHandler<TestEvent>), new TestEventHandler())
            };

            var eventBus = new EventBus(DependencyServiceMock.GetMock(handlers));
            var eventStore = new EventStore(eventBus, new InMemoryEventStore(),PerformanceCounterMock.GetMock());

            var events = new List<IDomainEvent>() {new TestEvent(Guid.NewGuid())};
            eventStore.Save(events, -1);

            eventStore.GetEventsByAggregateId(events[0].AggregateId).Should().BeEquivalentTo(events[0]);
        }

        [TestMethod]
        [Description("The eventstore GetEventsByAggregateId method should apply the filter correctly")]
        public void EventStoreShouldApplyTheFilterCorrectly()
        {
            var handlers = new List<DependecyDescriptor>
            {
                new DependecyDescriptor(typeof (IEventHandler<TestEvent>), new TestEventHandler())
            };

            var eventBus = new EventBus(DependencyServiceMock.GetMock(handlers));
            var eventStore = new EventStore(eventBus, new InMemoryEventStore(), PerformanceCounterMock.GetMock());

            var aggregateId = Guid.NewGuid();
            var events = new List<IDomainEvent>()
            {
                new TestEvent(aggregateId),
                new TestAlternativeEvent(Guid.NewGuid())
            };

            eventStore.Save(events, -1);

            eventStore.GetEventsByAggregateId(aggregateId).Should().BeEquivalentTo(events[0]);
        }

        [TestMethod]
        [Description("The eventstore should publish all incoming events to the event bus")]
        public void EventStoreShouldPublishAllSavedEventsToTheEventBus()
        {
            var result = String.Empty;

            var handlers = new List<DependecyDescriptor>
            {
                new DependecyDescriptor(typeof (IEventHandler<TestEvent>), new TestEventHandler((name) => result = name))
            };

            var eventBus = new EventBus(DependencyServiceMock.GetMock(handlers));
            var eventStore = new EventStore(eventBus, new InMemoryEventStore(), PerformanceCounterMock.GetMock());

            var events = new List<IDomainEvent>() {new TestEvent(Guid.NewGuid())};
            eventStore.Save(events, -1);

            result.Should().Be("TestEvent");
        }

        [TestMethod]
        [Description("The eventstore should not throw an exception when GetEventsByAggregateId is used on an empty list"
            )]
        public void EventStoreShouldNotThrowExceptionWhenFilterIsUsedOnEmptyEventList()
        {
            var handlers = new List<DependecyDescriptor>
            {
                new DependecyDescriptor(typeof (IEventHandler<TestEvent>), new TestEventHandler())
            };

            var eventBus = new EventBus(DependencyServiceMock.GetMock(handlers));
            var eventStore = new EventStore(eventBus, new InMemoryEventStore(), PerformanceCounterMock.GetMock());

            Action act = () => eventStore.GetEventsByAggregateId(Guid.NewGuid());
            act.ShouldNotThrow();
        }

        [TestMethod]
        [Description("The eventstore should process 20.000 events in one second")]
        public void EventStoreShouldProcess20000EventsInLessThan1Second()
        {
            var result = String.Empty;

            var handlers = new List<DependecyDescriptor>
            {
                new DependecyDescriptor(typeof (IEventHandler<TestEvent>), new TestEventHandler((name) => result += "0"))
            };

            var eventBus = new EventBus(DependencyServiceMock.GetMock(handlers));
            var eventStore = new EventStore(eventBus, new InMemoryEventStore(), PerformanceCounterMock.GetMock());

            Action act = () =>
            {
                for (var i = 0; i <= 19999; i++)
                {
                    var events = new List<IDomainEvent>() {new TestEvent(Guid.NewGuid())};
                    eventStore.Save(events, -1);
                }
            };

            act.ExecutionTime().ShouldNotExceed(new TimeSpan(0, 0, 1));
        }
    }
}