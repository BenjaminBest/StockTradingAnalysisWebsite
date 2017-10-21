using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockWknChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the wkn
        /// </summary>
        public string Wkn { get; private set; }

        public StockWknChangedEvent(Guid id, Type aggregateType, string wkn)
            : base(id, aggregateType)
        {
            Wkn = wkn;
        }
    }
}