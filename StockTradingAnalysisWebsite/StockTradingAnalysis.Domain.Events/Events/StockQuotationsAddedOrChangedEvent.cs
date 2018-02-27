using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    /// <summary>
    /// The event StockQuotationsAddedOrChangedEvent is fired when quotations were added for a stock.
    /// </summary>
    /// <seealso cref="DomainEvent" />
    public class StockQuotationsAddedOrChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets a stock price for one point in time
        /// </summary>
        public IEnumerable<IQuotation> Quotations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationsAddedOrChangedEvent" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="quotations">The quotations.</param>
        public StockQuotationsAddedOrChangedEvent(Guid id, Type aggregateType, IEnumerable<IQuotation> quotations)
            : base(id, aggregateType)
        {
            Quotations = quotations;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationsAddedOrChangedEvent"/> class.
        /// </summary>
        protected StockQuotationsAddedOrChangedEvent()
        {

        }
    }
}