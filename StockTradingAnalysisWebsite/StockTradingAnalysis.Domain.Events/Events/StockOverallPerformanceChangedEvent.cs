using StockTradingAnalysis.Interfaces.Events;
using System;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockOverallPerformanceChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the new profit that was made with transactions for this stock
        /// </summary>
        public decimal NewProfitAbsolute { get; private set; }

        public StockOverallPerformanceChangedEvent(Guid id, Type aggregateType, decimal newProfitAbsolute)
            : base(id, aggregateType)
        {
            NewProfitAbsolute = newProfitAbsolute;
        }
    }
}