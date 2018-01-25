using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockWknChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the wkn
        /// </summary>
        public string Wkn { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockWknChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="wkn">The WKN.</param>
        public StockWknChangedEvent(Guid id, Type aggregateType, string wkn)
            : base(id, aggregateType)
        {
            Wkn = wkn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockWknChangedEvent"/> class.
        /// </summary>
        protected StockWknChangedEvent()
        {
            
        }
    }
}