using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationWknChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the wkn
        /// </summary>
        public string Wkn { get; private set; }

        public CalculationWknChangedEvent(Guid id, Type aggregateType, string wkn)
            : base(id, aggregateType)
        {
            Wkn = wkn;
        }
    }
}