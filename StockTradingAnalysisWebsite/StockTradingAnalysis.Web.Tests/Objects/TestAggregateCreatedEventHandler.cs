using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAggregateCreatedEventHandler : IEventHandler<TestAggregateCreatedEvent>
    {
        public void Handle(TestAggregateCreatedEvent eventData)
        {
            TestDatabase.Items.Add(eventData.AggregateId, new Test(eventData.AggregateId) { Name = eventData.Name });
        }
    }
}