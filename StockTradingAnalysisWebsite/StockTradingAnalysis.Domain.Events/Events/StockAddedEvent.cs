using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StockAddedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the wkn
        /// </summary>
        public string Wkn { get; private set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets if the stock is used when buying or selling
        /// </summary>
        public string LongShort { get; private set; }

        /// <summary>
        /// Gets the quotations for this stock
        /// </summary>
        public IEnumerable<IQuotation> Quotations { get; private set; }

        public StockAddedEvent(Guid id, Type aggregateType, string name, string wkn, string type, string longShort, IEnumerable<IQuotation> quotations)
            : base(id, aggregateType)
        {
            Name = name;
            Wkn = wkn;
            Type = type;
            LongShort = longShort;
            Quotations = quotations;
        }
    }
}