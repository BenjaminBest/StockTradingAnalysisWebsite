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

        public CalculationInitialSlChangedEvent(Guid id, Type aggregateType, decimal initialSl)
            : base(id, aggregateType)
        {
            InitialSl = initialSl;
        }
    }
}