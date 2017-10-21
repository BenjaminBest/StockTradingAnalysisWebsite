using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using System;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestSnapshotAggregate : AggregateRoot,
        IHandle<TestAggregateCreatedEvent>,
        IHandle<TestAggregateChangedEvent>,
        ISnapshotOriginator
    {
        private string WKN { get; set; }
        private string Name { get; set; }
        private bool IsDividend { get; set; }

        public override Guid Id { get; protected set; }

        public TestSnapshotAggregate()
        {
        }

        public TestSnapshotAggregate(Guid id, string name)
        {
            ApplyChange(new TestAggregateCreatedEvent(id, typeof(TestSnapshotAggregate), name));
        }

        public void ChangeName(string name)
        {
            ApplyChange(new TestAggregateChangedEvent(Id, typeof(TestSnapshotAggregate), IsDividend, name));
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

        //Snapshots
        public Guid OriginatorId => Id;

        public SnapshotBase GetSnapshot()
        {
            return new TestAggregateSnapshot(Id, Version, WKN, Name, IsDividend);
        }

        public void SetSnapshot(SnapshotBase snapshot)
        {
            var item = snapshot as TestAggregateSnapshot;

            if (item == null)
                return;

            Version = item.Version;
            Id = item.AggregateId;

            IsDividend = item.IsDividend;
            WKN = item.WKN;
            Name = item.Name;
        }
    }
}