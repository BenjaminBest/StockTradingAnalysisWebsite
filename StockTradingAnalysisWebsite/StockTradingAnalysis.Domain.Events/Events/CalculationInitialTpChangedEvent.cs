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

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationInitialTpChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="initialTp">The initial tp.</param>
        public CalculationInitialTpChangedEvent(Guid id, Type aggregateType, decimal initialTp)
            : base(id, aggregateType)
        {
            InitialTp = initialTp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationInitialTpChangedEvent"/> class.
        /// </summary>
        protected CalculationInitialTpChangedEvent()
        {

        }
    }
}