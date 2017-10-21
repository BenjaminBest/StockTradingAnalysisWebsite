using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockLongShortChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets if the stock is used when buying or selling
        /// </summary>
        public string LongShort { get; private set; }

        public StockLongShortChangedEvent(Guid id, Type aggregateType, string longShort)
            : base(id, aggregateType)
        {
            LongShort = longShort;
        }
    }
}