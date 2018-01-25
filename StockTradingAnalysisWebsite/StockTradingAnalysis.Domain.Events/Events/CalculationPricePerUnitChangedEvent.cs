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

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationPricePerUnitChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="pricePerUnit">The price per unit.</param>
        public CalculationPricePerUnitChangedEvent(Guid id, Type aggregateType, decimal pricePerUnit)
            : base(id, aggregateType)
        {
            PricePerUnit = pricePerUnit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationPricePerUnitChangedEvent"/> class.
        /// </summary>
        protected CalculationPricePerUnitChangedEvent()
        {

        }
    }
}