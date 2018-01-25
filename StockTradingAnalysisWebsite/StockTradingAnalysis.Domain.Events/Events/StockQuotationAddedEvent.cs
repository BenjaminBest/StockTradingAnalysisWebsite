using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockQuotationAddedEvent : DomainEvent
    {
        /// <summary>
        /// Gets a stock price for one point in time
        /// </summary>
        public IQuotation Quotation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationAddedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="quotation">The quotation.</param>
        public StockQuotationAddedEvent(Guid id, Type aggregateType, IQuotation quotation)
            : base(id, aggregateType)
        {
            Quotation = quotation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationAddedEvent"/> class.
        /// </summary>
        protected StockQuotationAddedEvent()
        {

        }
    }
}