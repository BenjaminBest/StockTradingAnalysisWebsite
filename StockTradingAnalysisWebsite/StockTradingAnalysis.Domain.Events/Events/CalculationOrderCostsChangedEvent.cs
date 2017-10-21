using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationOrderCostsChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the order costs
        /// </summary>
        public decimal OrderCosts { get; private set; }

        public CalculationOrderCostsChangedEvent(Guid id, Type aggregateType, decimal orderCosts)
            : base(id, aggregateType)
        {
            OrderCosts = orderCosts;
        }
    }
}