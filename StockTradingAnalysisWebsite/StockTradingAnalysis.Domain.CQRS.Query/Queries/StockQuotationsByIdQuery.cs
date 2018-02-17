using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The StockQuotationsByIdQuery is used to return all quotes for a given stock
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{IEnumerable{IQuotation}}" />
    public class StockQuotationsByIdQuery : IQuery<IEnumerable<IQuotation>>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationsByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public StockQuotationsByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}