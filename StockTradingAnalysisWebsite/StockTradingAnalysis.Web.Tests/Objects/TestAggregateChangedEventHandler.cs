using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAggregateChangedEventHandler : IEventHandler<TestAggregateChangedEvent>
    {
        public void Handle(TestAggregateChangedEvent eventData)
        {            
            if(TestDatabase.Items.ContainsKey(eventData.AggregateId))
            {
                TestDatabase.Items[eventData.AggregateId].IsDividend = eventData.IsDividend;
                TestDatabase.Items[eventData.AggregateId].Name = eventData.Name;
                TestDatabase.Items[eventData.AggregateId].OriginalVersion = eventData.Version;
            }
        }
    }
}