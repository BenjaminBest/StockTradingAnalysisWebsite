using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.EventSourcing.DomainContext;
using StockTradingAnalysis.EventSourcing.Exceptions;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class SnapshotTests
    {
        [TestMethod]
        [Description("Snapshot store should not throw an exception when no snapshots are available but retrieved")]
        public void SnapshotStoreShouldNotThrowExceptionWhenNoSnapshotIsAvailable()
        {
            var persistentSnapshotStore = new InMemorySnapshotStore();
            var snapshotStore = new SnapshotStore(persistentSnapshotStore);

            Action act = () => snapshotStore.GetSnapshot(Guid.NewGuid());
            act.ShouldNotThrow();
        }

        [TestMethod]
        [Description("Snapshot store should save the aggregate snapshot to persistent storage")]
        public void SnapshotStoreSavesSnapshotToPersistentStore()
        {
            var persistentSnapshotStore = new InMemorySnapshotStore();
            var snapshotStore = new SnapshotStore(persistentSnapshotStore);

            var guid = Guid.NewGuid();
            var aggregate = new TestSnapshotAggregate(guid, "Test aggregate");

            snapshotStore.SaveSnapshot(aggregate);

            persistentSnapshotStore.Find(guid).FirstOrDefault().Should().NotBeNull();
            persistentSnapshotStore.Find(guid).FirstOrDefault().AggregateId.Should().Be(guid);
        }

        [TestMethod]
        [Description("Snapshot store loads the aggregate snapshot from persistens storage")]
        public void SnapshotStoreLoadsSnapshotFromPersistentStore()
        {
            var persistentSnapshotStore = new InMemorySnapshotStore();
            var snapshotStore = new SnapshotStore(persistentSnapshotStore);

            var guid = Guid.NewGuid();
            var aggregate = new TestSnapshotAggregate(guid, "Test aggregate");

            snapshotStore.SaveSnapshot(aggregate);

            snapshotStore.GetSnapshot(guid).Should().NotBeNull();
            snapshotStore.GetSnapshot(guid).AggregateId.Should().Be(guid);
        }

        [TestMethod]
        [Description("Snapshot store should return the correct type of the aggregate when casted")]
        public void SnapshotStoreShouldReturnTheCorrectAggregateType()
        {
            var persistentSnapshotStore = new InMemorySnapshotStore();
            var snapshotStore = new SnapshotStore(persistentSnapshotStore);

            var guid = Guid.NewGuid();
            var aggregate = new TestSnapshotAggregate(guid, "Test aggregate");

            snapshotStore.SaveSnapshot(aggregate);

            var snapshot = snapshotStore.GetSnapshot(guid) as TestAggregateSnapshot;

            snapshot.Should().NotBeNull();
        }

        [TestMethod]
        [Description("Snapshot from store can be successfully applied in the aggregate")]
        public void SnapshotShouldBeSuccessfullyAppliedToAggregate()
        {
            var persistentSnapshotStore = new InMemorySnapshotStore();
            var snapshotStore = new SnapshotStore(persistentSnapshotStore);

            var guid = Guid.NewGuid();
            const string name = "Test aggregate";
            var aggregate = new TestSnapshotAggregate(guid, name);

            snapshotStore.SaveSnapshot(aggregate);
            var snapshot = snapshotStore.GetSnapshot(guid) as TestAggregateSnapshot;

            var newAggregate = new TestSnapshotAggregate(Guid.NewGuid(), String.Empty);
            newAggregate.SetSnapshot(snapshot);

            newAggregate.Id.Should().Be(guid);
            newAggregate.ReturnNameForTest().Should().Be(name);
        }

        [TestMethod]
        [Description("Snapshot processor should not create snapshot if no events are available (empty snapshots)")]
        public void SnapshotProcessorShouldNotCreateSnapshotIfNoEventIsAvailable()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore);

            var guid = Guid.NewGuid();
            snapshotProcessor.CreateSnapshot(guid, typeof(TestSnapshotAggregate));

            memorySnapshotStore.Find(guid).Should().BeEmpty();
        }

        [TestMethod]
        [Description("Snapshot processor should not create snapshot if aggregate doens't support snapshots")]
        public void SnapshotProcessorShouldNotCreateSnapshotIfAggregateDoesntSupportThis()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore);

            var guid = Guid.NewGuid();
            snapshotProcessor.CreateSnapshot(guid, typeof(TestAggregate));

            memorySnapshotStore.Find(guid).Should().BeEmpty();
        }

        [TestMethod]
        [Description("Snapshot processor should not create snapshot before threshold is reached")]
        public void SnapshotProcessorShouldNotCreateSnapshotBeforeThresholdIsReached()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 3);

            var guid = Guid.NewGuid();
            var aggregate = new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0");
            aggregate.ChangeName("Test Snapshot Aggregate v1");

            repository.Save(aggregate, -1);

            memorySnapshotStore.Find(guid).Should().BeEmpty();
        }

        [TestMethod]
        [Description("Snapshot processor should create snapshot after threshold is reached")]
        public void SnapshotProcessorShouldCreateSnapshotAfterThresholdIsReached()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 3);

            var guid = Guid.NewGuid();

            repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);

            var aggregate = repository.GetById(guid);
            aggregate.ChangeName("Test Snapshot Aggregate v1");
            aggregate.ChangeName("Test Snapshot Aggregate v2");
            aggregate.ChangeName("Test Snapshot Aggregate v3 (Snapshot)");

            repository.Save(aggregate, 0);
            memorySnapshotStore.Find(guid).Should().NotBeEmpty();
        }

        [TestMethod]
        [Description("Aggregate should be valid when loaded with existent snapshot and no new events")]
        public void AggregateShouldBeValidWhenLoadedWithExistentSnapshotAndNoNewEvents()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 3);

            var guid = Guid.NewGuid();

            repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);

            const string name = "Test Snapshot Aggregate v3 (Snapshot)";
            var aggregate = repository.GetById(guid);
            aggregate.ChangeName("Test Snapshot Aggregate v1");
            aggregate.ChangeName("Test Snapshot Aggregate v2");
            aggregate.ChangeName(name);

            repository.Save(aggregate, 0);

            repository.GetById(guid).ReturnNameForTest().Should().Be(name);
        }

        [TestMethod]
        [Description("Aggregate should be valid when loaded with existent snapshot and new events")]
        public void AggregateShouldBeValidWhenLoadedWithExistentSnapshotAndNewEvents()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 3);

            var guid = Guid.NewGuid();

            repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);

            var aggregate = repository.GetById(guid);
            aggregate.ChangeName("Test Snapshot Aggregate v1");
            aggregate.ChangeName("Test Snapshot Aggregate v2");
            aggregate.ChangeName("Test Snapshot Aggregate v3 (Snapshot)");

            repository.Save(aggregate, 0);

            const string name = "Test Snapshot Aggregate v4";
            var aggregateV4 = repository.GetById(guid);
            aggregateV4.ChangeName(name);
            repository.Save(aggregateV4, 3);

            repository.GetById(guid).ReturnNameForTest().Should().Be(name);
        }

        [TestMethod]
        [Description("Repository should be able to load/save 20.000 Aggregates within 10 sec with snapshots enabled")]
        public void RepositoryShouldLoadAndSave20000AggregatesWithSnapShotsEnabledIn60Seconds()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 100);

            var guid = Guid.NewGuid();

            repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);

            Action act = () =>
            {
                for (var i = 1; i <= 19999; i++)
                {
                    var aggregate = repository.GetById(guid);
                    aggregate.ChangeName("Test Snapshot Aggregate v" + i);
                    repository.Save(aggregate, i - 1);
                }
            };

            act.ExecutionTime().ShouldNotExceed(new TimeSpan(0, 0, 60));
        }

        [TestMethod]
        [Description("Repository should throw exception if snapshots aren't supported")]
        public void RepositoryShouldThrowExceptionIfNotSupported()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 3);

            Action act = () => repository.GetOriginator(Guid.NewGuid());
            act.ShouldThrow<SnapshotNotSupportedException>();
        }


        [TestMethod]
        [Description("Snapshot Processor should need snapshot when threashold is exceeded")]
        public void SnapshotProcesserIsSnapshotNeededShouldReturnTrueWhenThreasholdIsExceeded()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 3);

            var guid = Guid.NewGuid();

            repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);

            var aggregate = repository.GetById(guid);
            aggregate.ChangeName("Test Snapshot Aggregate v1");
            aggregate.ChangeName("Test Snapshot Aggregate v2");
            aggregate.ChangeName("Test Snapshot Aggregate v3 (Snapshot)");
            repository.Save(aggregate, 0);

            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 5).Should().BeFalse();
            //Because aggregate version stays 3, every new aggregate version needs a snapshot
            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 6).Should().BeTrue();
            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 7).Should().BeTrue();
            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 8).Should().BeTrue();
            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 9).Should().BeTrue();
        }

        [TestMethod]
        [Description("Snapshot Processor should correctly calculate snapshot intervals")]
        public void SnapshotProcesserIsSnapshotNeededShouldCorrectlyCalculateThreasholdIntervalsWhenAggregateVersionIsIncreased()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 3);

            var guid = Guid.NewGuid();

            repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);

            var aggregate = repository.GetById(guid);
            aggregate.ChangeName("Test Snapshot Aggregate v1");
            aggregate.ChangeName("Test Snapshot Aggregate v2");
            aggregate.ChangeName("Test Snapshot Aggregate v3 (Snapshot)");
            repository.Save(aggregate, 0);

            aggregate.ChangeName("Test Snapshot Aggregate v4");
            repository.Save(aggregate, 3);

            aggregate.ChangeName("Test Snapshot Aggregate v5");
            repository.Save(aggregate, 4);

            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 5).Should().BeFalse();
            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 6).Should().BeTrue(); // Needed because last snapshot version is 3

            aggregate.ChangeName("Test Snapshot Aggregate v6 (Snapshot)");
            repository.Save(aggregate, 5);

            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 6).Should().BeFalse(); //Not needed because snapshot was taken of v5

            aggregate.ChangeName("Test Snapshot Aggregate v7");
            repository.Save(aggregate, 6);

            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 7).Should().BeFalse();

            aggregate.ChangeName("Test Snapshot Aggregate v8");
            repository.Save(aggregate, 7);

            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 8).Should().BeFalse();
            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 9).Should().BeTrue(); // Needed because last snapshot version is 6

            aggregate.ChangeName("Test Snapshot Aggregate v9 (snapshot)");
            repository.Save(aggregate, 8);

            snapshotProcessor.IsSnapshotNeeded(guid, typeof(TestSnapshotAggregate), 9).Should().BeFalse(); //Not needed because snapshot was taken of v9
        }

        [TestMethod]
        [Description("Snapshot Processor should be able to work correctly when multiple events are committed at once")]
        public void SnapshotProcesserIsSnapshotNeededShouldReturnCorrectValuesWhenMultipleEventsAreCommittedAtOnce()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            InMemorySnapshotStore memorySnapshotStore;
            var repository = EnvironmentMock.CreateEnvironment<TestSnapshotAggregate>(out eventBus, out eventStore,
                out snapshotStore, out snapshotProcessor, out memorySnapshotStore, 3);

            var guid = Guid.NewGuid();

            repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);

            var aggregate = repository.GetById(guid);
            aggregate.ChangeName("Test Snapshot Aggregate v1");
            aggregate.ChangeName("Test Snapshot Aggregate v2");
            aggregate.ChangeName("Test Snapshot Aggregate v3 (Snapshot)");
            aggregate.ChangeName("Test Snapshot Aggregate v4");
            aggregate.ChangeName("Test Snapshot Aggregate v5");
            aggregate.ChangeName("Test Snapshot Aggregate v6 (Snapshot)");
            aggregate.ChangeName("Test Snapshot Aggregate v7");
            aggregate.ChangeName("Test Snapshot Aggregate v8");
            aggregate.ChangeName("Test Snapshot Aggregate v9 (Snapshot)");
            repository.Save(aggregate, 0);

            aggregate.GetSnapshot().Version.Should().Be(9);
            memorySnapshotStore.All().Count().Should().Be(1);
        }
    }
}