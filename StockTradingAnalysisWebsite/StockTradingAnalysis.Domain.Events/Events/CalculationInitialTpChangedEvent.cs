using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationInitialTpChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the initial take profit
        /// </summary>
        public decimal InitialTp { get; private set; }

        public CalculationInitialTpChangedEvent(Guid id, Type aggregateType, decimal initialTp)
            : base(id, aggregateType)
        {
            InitialTp = initialTp;
        }
    }
}