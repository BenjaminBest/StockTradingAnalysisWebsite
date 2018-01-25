using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAggregateChangedEvent : DomainEvent
    {
        public bool IsDividend { get; private set; }
        public string Name { get; private set; }

        public TestAggregateChangedEvent(Guid aggregateId, Type aggregateType, bool isDividend, string name)
            : base(aggregateId, aggregateType)
        {
            IsDividend = isDividend;
            Name = name;
        }

        protected TestAggregateChangedEvent()
        {

        }
    }
}