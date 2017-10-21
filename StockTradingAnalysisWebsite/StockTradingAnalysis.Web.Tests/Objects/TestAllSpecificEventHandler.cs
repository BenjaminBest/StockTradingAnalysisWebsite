using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAllSpecificEventHandler : IEventHandler<TestEvent>, IEventHandler<TestAlternativeEvent>
    {
        private readonly Action<string> _action;

        public TestAllSpecificEventHandler(Action<string> action)
        {
            _action = action;
        }

        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void Handle(TestEvent eventData)
        {
            _action(eventData.EventName);
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