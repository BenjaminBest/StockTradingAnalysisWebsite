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

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationOrderCostsChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="orderCosts">The order costs.</param>
        public CalculationOrderCostsChangedEvent(Guid id, Type aggregateType, decimal orderCosts)
            : base(id, aggregateType)
        {
            OrderCosts = orderCosts;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationOrderCostsChangedEvent"/> class.
        /// </summary>
        protected CalculationOrderCostsChangedEvent()
        {

        }
    }
}