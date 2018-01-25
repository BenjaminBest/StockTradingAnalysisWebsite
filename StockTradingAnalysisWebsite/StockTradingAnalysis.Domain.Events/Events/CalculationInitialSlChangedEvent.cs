using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationInitialSlChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the initial stop loss
        /// </summary>
        public decimal InitialSl { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationInitialSlChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="initialSl">The initial sl.</param>
        public CalculationInitialSlChangedEvent(Guid id, Type aggregateType, decimal initialSl)
            : base(id, aggregateType)
        {
            InitialSl = initialSl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationInitialSlChangedEvent"/> class.
        /// </summary>
        protected CalculationInitialSlChangedEvent()
        {

        }
    }
}