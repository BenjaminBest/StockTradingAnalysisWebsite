using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockLongShortChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets if the stock is used when buying or selling
        /// </summary>
        public string LongShort { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockLongShortChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="longShort">The long short.</param>
        public StockLongShortChangedEvent(Guid id, Type aggregateType, string longShort)
            : base(id, aggregateType)
        {
            LongShort = longShort;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockLongShortChangedEvent"/> class.
        /// </summary>
        protected StockLongShortChangedEvent()
        {
            
        }
    }
}