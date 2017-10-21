using System;

namespace StockTradingAnalysis.EventSourcing.Exceptions
{
    public class EventStoreInitializeException : Exception
    {
        public EventStoreInitializeException(string message)
            : base(message)
        {
        }
    }
}