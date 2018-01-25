using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAlternativeEvent : DomainEvent
    {
        public TestAlternativeEvent(Guid aggregateId)
            : base(aggregateId, typeof(TestAggregate))
        {

        }

        protected TestAlternativeEvent()
        {

        }
    }
}