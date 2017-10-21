using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class AggregateTests
    {
        [TestMethod]
        [Description("Aggregate should apply the parameter id provided by the ctor")]
        public void AggregateShouldApplyConstructorParameterId()
        {
            var guid = Guid.NewGuid();
            var aggregate = new TestAggregate(guid, String.Empty);

            aggregate.Id.Should().Be(guid);
        }

        [TestMethod]
        [Description("Aggregate should same an event to the changes list when instanciated")]
        public void AggregateShouldCreateEventWhenInstanciated()
        {
            var aggregate = new TestAggregate(Guid.NewGuid(), String.Empty);

            aggregate.GetUncommittedChanges().Count().Should().BeGreaterThan(0);
            aggregate.GetUncommittedChanges().FirstOrDefault().EventName.Should().Be("TestAggregateCreatedEvent");
        }

        [TestMethod]
        [Description("Aggregate should have pending changes when instanciated")]
        public void AggregateShouldHavePendingChangesWhenInstanciated()
        {
            var aggregate = new TestAggregate(Guid.NewGuid(), String.Empty);

            aggregate.HasPendingChanges().Should().Be(true);
        }

        [TestMethod]
        [Description("Aggregate should clear pending changes when marked as commited")]
        public void AggregateShouldClearPendingChangesWhenMarkedAsCommitted()
        {
            var aggregate = new TestAggregate(Guid.NewGuid(), String.Empty);
            aggregate.MarkChangesAsCommited();

            aggregate.HasPendingChanges().Should().Be(false);
        }

        [TestMethod]
        [Description("Aggregate should have no pending changes when loaded from history")]
        public void AggregateShouldHaveNoPendingChangesWhenLoadedFromHistory()
        {
            var events = new List<IDomainEvent>();
            events.Add(new TestAggregateCreatedEvent(Guid.NewGuid(), typeof (TestAggregate), String.Empty));

            var aggregate = new TestAggregate();
            aggregate.LoadFromHistory(events);

            aggregate.HasPendingChanges().Should().Be(false);
        }

        [TestMethod]
        [Description("Aggregate should have the correct values (given by the event) when loaded from history")]
        public void AggregateShouldHaveTheCorrectValuesWhenLoadedFromHistory()
        {
            var guid = Guid.NewGuid();
            var events = new List<IDomainEvent>();
            events.Add(new TestAggregateCreatedEvent(guid, typeof (TestAggregate), String.Empty));

            var aggregate = new TestAggregate();
            aggregate.LoadFromHistory(events);

            aggregate.Id.Should().Be(guid);
        }
    }
}