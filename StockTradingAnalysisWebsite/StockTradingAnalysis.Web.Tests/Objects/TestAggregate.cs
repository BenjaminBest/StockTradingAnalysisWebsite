using System;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAggregate : AggregateRoot,
        IHandle<TestAggregateCreatedEvent>,
        IHandle<TestAggregateChangedEvent>
    {
        private string Name { get; set; }
        private bool IsDividend { get; set; }

        public override Guid Id { get; protected set; }

        public TestAggregate()
        {
        }

        public TestAggregate(Guid id, string name)
        {
            ApplyChange(new TestAggregateCreatedEvent(id, typeof (TestAggregate), name));
        }

        public void ChangeName(string name)
        {
            ApplyChange(new TestAggregateChangedEvent(Id, typeof (TestAggregate), IsDividend, name));
        }

        public void Handle(TestAggregateCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
        }

        public void Handle(TestAggregateChangedEvent @event)
        {
            IsDividend = @event.IsDividend;
            Name = @event.Name;
        }

        //Only for testing purposes
        public string ReturnNameForTest()
        {
            return Name;
        }
    }
}