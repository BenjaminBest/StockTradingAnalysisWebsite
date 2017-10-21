using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestEventHandler : IEventHandler<TestEvent>
    {
        private readonly Action<string> _action;

        public TestEventHandler()
        {
        }

        public TestEventHandler(Action<string> action)
        {
            _action = action;
        }

        public void Handle(TestEvent eventData)
        {
            if (_action != null)
                _action(eventData.EventName);
        }
    }
}