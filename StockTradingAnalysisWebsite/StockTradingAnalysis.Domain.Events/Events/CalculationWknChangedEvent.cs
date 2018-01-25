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

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationWknChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="wkn">The WKN.</param>
        public CalculationWknChangedEvent(Guid id, Type aggregateType, string wkn)
            : base(id, aggregateType)
        {
            Wkn = wkn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationWknChangedEvent"/> class.
        /// </summary>
        protected CalculationWknChangedEvent()
        {

        }
    }
}