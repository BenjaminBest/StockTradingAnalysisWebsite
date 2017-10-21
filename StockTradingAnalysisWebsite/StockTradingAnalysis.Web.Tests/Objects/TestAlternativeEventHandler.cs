using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAlternativeEventHandler : IEventHandler<TestAlternativeEvent>
    {
        private readonly Action<string> _action;

        public TestAlternativeEventHandler(Action<string> action)
        {
            _action = action;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TestAlternativeEvent eventData)
        {
            _action(eventData.EventName);
        }
    }
}