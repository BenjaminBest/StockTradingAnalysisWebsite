using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.EventSourcing.DomainContext;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;
using System;
using System.Linq;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class DatastoreTests
    {
        [TestMethod]
        [Description("Datastore should load and save events with no eventual consistency problem")]
        public void DatastoreShouldLoadAndSaveWithNoEventualConsistency()
        {
            EventBus eventBus;
            EventStore eventStore;
            SnapshotStore snapshotStore;
            SnapshotProcessor snapshotProcessor;
            DocumentDatabaseEventStore documentEventStore;
            DocumentDatabaseSnapshotStore documentSnapshotStore;

            var repository = EnvironmentMock.CreateDatabaseEnvironment<TestSnapshotAggregate>(out eventBus,
                out eventStore,
                out snapshotStore, out documentEventStore, out snapshotProcessor, out documentSnapshotStore, 100);

            var guid = Guid.NewGuid();

            repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);
            var test = repository.GetById(guid);
            test.Id.Should().NotBe(Guid.Empty);

            Action act = () =>
            {
                for (var i = 1; i <= 299; i++)
                {
                    var aggregate = repository.GetById(guid);
                    aggregate.ChangeName("Test Snapshot Aggregate v" + i);
                    repository.Save(aggregate, i - 1);
                }
            };

            act.ShouldNotThrow();

            documentEventStore.Find(guid).Should().NotBeNull();
            documentEventStore.Find(guid).Count().Should().Be(300);
            documentSnapshotStore.Find(guid).Should().NotBeNull();
            documentSnapshotStore.Find(guid).Count().Should().Be(2);
        }
    }
}