using System;

namespace StockTradingAnalysis.Domain.Events.Exceptions
{
    public class EventHandlerException : Exception
    {
        public Type EventHandlerType { get; private set; }

        public EventHandlerException(string message, Type eventHandlerType)
            : base(message)
        {
            EventHandlerType = eventHandlerType;
        }
    }
}