using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationPricePerUnitChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the price per unit
        /// </summary>
        public decimal PricePerUnit { get; private set; }

        public CalculationPricePerUnitChangedEvent(Guid id, Type aggregateType, decimal pricePerUnit)
            : base(id, aggregateType)
        {
            PricePerUnit = pricePerUnit;
        }
    }
}