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

        public StockQuotationChangedEvent(Guid id, Type aggregateType, IQuotation quotation)
            : base(id, aggregateType)
        {
            Quotation = quotation;
        }
    }
}