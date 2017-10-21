using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        [Description("Repository should persist event to eventstore when aggregate was saved")]
        public void RepositoryShouldPersistEventToEventStoreWhenAggregateWasSaved()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();
            var aggregate = new TestAggregate(guid, String.Empty);

            repository.Save(aggregate, -1);

            eventStore.GetEventsByAggregateId(guid).Count().ShouldBeEquivalentTo(1);
        }

        [TestMethod]
        [Description("Repository should return new aggregate if aggregate wasnt saved before")]
        public void RepositoryShouldReturnNewAggregateIfItWasntSavedBefore()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();

            repository.GetById(guid).Should().NotBeNull();
            repository.GetById(guid).Id.Should().Be(Guid.Empty);
        }

        [TestMethod]
        [Description("Repository should return previously saved aggregate with correct values")]
        public void RepositoryShouldReturnPreviouslySavedAggregate()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();
            const string name = "Initial Aggregate Name";
            var aggregate = new TestAggregate(guid, String.Empty);
            aggregate.ChangeName(name);

            repository.Save(aggregate, -1);

            repository.GetById(guid).ReturnNameForTest().Should().Be(name);
        }

        [TestMethod]
        [Description(
            "Repository should return previously saved aggregate with multiple changes applied in the right order")]
        public void RepositoryShouldReturnPreviouslySavedAggregateWithChangesAppliedInTheRightOrder()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();
            const string name = "Initial Aggregate Name ";
            var aggregate = new TestAggregate(guid, String.Empty);
            aggregate.ChangeName(name + 1);
            aggregate.ChangeName(name + 2);
            aggregate.ChangeName(name + 1);
            aggregate.ChangeName(name + 3);

            repository.Save(aggregate, -1);

            repository.GetById(guid).ReturnNameForTest().Should().Be(name + 3);
        }

        [TestMethod]
        [Description("Repository save should initiate the event handlers to update the read model")]
        public void RepositorySaveShouldInitiateEventHandlersToUpdateReadModel()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();
            const string name = "Initial Aggregate Name";
            var aggregate = new TestAggregate(guid, String.Empty);
            aggregate.ChangeName(name);

            repository.Save(aggregate, -1);

            TestDatabase.Items[guid].Name.Should().Be(name);
        }

        [TestMethod]
        [Description("Repository should mark all aggregates changes as committed")]
        public void RepositorySaveShouldMarkAllAggregateChangesAsCommitted()
        {
            EventBus eventBus;
            EventStore eventStore;
            var repository = EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var guid = Guid.NewGuid();
            const string name = "Initial Aggregate Name";
            var aggregate = new TestAggregate(guid, String.Empty);
            aggregate.ChangeName(name);

            repository.Save(aggregate, -1);

            repository.GetById(guid).HasPendingChanges().Should().Be(false);
        }
    }
}