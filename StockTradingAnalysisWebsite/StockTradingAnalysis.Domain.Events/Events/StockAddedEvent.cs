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

        /// <summary>
        /// Initializes a new instance of the <see cref="StockAddedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="name">The name.</param>
        /// <param name="wkn">The WKN.</param>
        /// <param name="type">The type.</param>
        /// <param name="longShort">The long short.</param>
        /// <param name="quotations">The quotations.</param>
        public StockAddedEvent(Guid id, Type aggregateType, string name, string wkn, string type, string longShort, IEnumerable<IQuotation> quotations)
            : base(id, aggregateType)
        {
            Name = name;
            Wkn = wkn;
            Type = type;
            LongShort = longShort;
            Quotations = quotations;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockAddedEvent"/> class.
        /// </summary>
        protected StockAddedEvent()
        {
            
        }
    }
}