using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using System;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockQuotationChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets a stock price for one point in time
        /// </summary>
        public IQuotation Quotation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="quotation">The quotation.</param>
        public StockQuotationChangedEvent(Guid id, Type aggregateType, IQuotation quotation)
            : base(id, aggregateType)
        {
            Quotation = quotation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationChangedEvent"/> class.
        /// </summary>
        protected StockQuotationChangedEvent()
        {
            
        }
    }
}