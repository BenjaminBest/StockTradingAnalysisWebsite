using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.EventSourcing.Exceptions;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class VersioningTests
    {
        [TestMethod]
        [Description("The version of the first event, the aggregate create event, should be zero")]
        public void VersionOfAggregateCreateEventShouldBeZero()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();
            var aggregate = new TestAggregate(guid, "Test Aggregate 1");

            repository.Save(aggregate, -1);

            eventStore.GetEventsByAggregateId(guid).FirstOrDefault().Version.Should().Be(0);
        }

        [TestMethod]
        [Description("The version of the aggregate should be published by the eventstore to the subscribers")]
        public void VersionOfAggregateShouldBePublishedByEventByStore()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();

            repository.Save(new TestAggregate(guid, "Test Aggregate 1"), -1);

            TestDatabase.Items[guid].OriginalVersion.Should().Be(0);
        }

        [TestMethod]
        [Description("The version of the aggregate should be increased with newly published events")]
        public void VersionOfAggregateShouldBeIncreatedWithNewAddedEvents()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();

            repository.Save(new TestAggregate(guid, "Test Aggregate 1"), -1);

            var expectedVersion = TestDatabase.Items[guid].OriginalVersion;

            var loadedAggregate = repository.GetById(guid);
            loadedAggregate.ChangeName("New test Aggregate 1");
            repository.Save(loadedAggregate, expectedVersion);

            eventStore.GetEventsByAggregateId(guid)
                .OrderByDescending(e => e.Version)
                .FirstOrDefault()
                .Version.Should()
                .Be(expectedVersion + 1);
        }

        [TestMethod]
        [Description("The version of the aggregates should be independently increased")]
        public void VersionOfAggregatesShouldBeIndependentlyIncreased()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            repository.Save(new TestAggregate(guid1, "Test Aggregate 1"), -1);
            repository.Save(new TestAggregate(guid2, "Test Aggregate 2"), -1);

            var expectedVersion1 = TestDatabase.Items[guid1].OriginalVersion;
            var expectedVersion2 = TestDatabase.Items[guid2].OriginalVersion;

            var loadedAggregate1 = repository.GetById(guid1);

            loadedAggregate1.ChangeName("New test Aggregate 1");
            repository.Save(loadedAggregate1, expectedVersion1);

            eventStore.GetEventsByAggregateId(guid1)
                .OrderByDescending(e => e.Version)
                .FirstOrDefault()
                .Version.Should()
                .Be(expectedVersion1 + 1);
            eventStore.GetEventsByAggregateId(guid2)
                .OrderByDescending(e => e.Version)
                .FirstOrDefault()
                .Version.Should()
                .Be(expectedVersion2);
        }

        [TestMethod]
        [Description("The eventstore should throw a concurrency exception when expected version is wrong")]
        public void EventStoreShouldThrowConcurrencyExceptionWhenExpectedVersionIsWrong()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();

            repository.Save(new TestAggregate(guid, "Test Aggregate 1"), -1);

            var expectedVersion = TestDatabase.Items[guid].OriginalVersion;

            var loadedAggregate = repository.GetById(guid);
            loadedAggregate.ChangeName("New test Aggregate 1");

            repository.Save(loadedAggregate, expectedVersion);

            var newLoadedAggregate = repository.GetById(guid);
            newLoadedAggregate.ChangeName("Newer test Aggregate 1");

            Action act = () => repository.Save(newLoadedAggregate, expectedVersion);

            act.ShouldThrow<ConcurrencyException>();
        }

        [TestMethod]
        [Description("The version of the aggregate should be equal when events were applied to an non saved aggregate")]
        public void VersionOfAggregateShouldBeInSyncWithNonSavedAppliedEvents()
        {
            var guid = Guid.NewGuid();

            var aggregate = new TestSnapshotAggregate(guid, "Test Aggregate 1");
            aggregate.ChangeName("New test Aggregate Version 1");
            aggregate.ChangeName("New test Aggregate Version 2");
            aggregate.ChangeName("New test Aggregate Version 3");
            aggregate.ChangeName("New test Aggregate Version 4");
            aggregate.ChangeName("New test Aggregate Version 5");
            aggregate.ChangeName("New test Aggregate Version 6");
            aggregate.ChangeName("New test Aggregate Version 7");

            aggregate.GetSnapshot().Version.Should().Be(7);
        }
    }
}