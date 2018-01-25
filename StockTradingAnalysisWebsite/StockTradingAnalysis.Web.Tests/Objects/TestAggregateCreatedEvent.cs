using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAggregateCreatedEvent : DomainEvent
    {
        public string Name { get; private set; }

        public TestAggregateCreatedEvent(Guid aggregateId, Type aggregateType, string name)
            : base(aggregateId, aggregateType)
        {
            Name = name;
        }

        protected TestAggregateCreatedEvent()
        {

        }
    }
}