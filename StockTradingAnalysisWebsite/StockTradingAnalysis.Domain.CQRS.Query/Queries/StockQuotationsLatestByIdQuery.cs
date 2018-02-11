using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The query StockQuotationsLastByIdQuery returns the lastest quote of a given stock.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{IQuotation}" />
    public class StockQuotationsLatestByIdQuery : IQuery<IQuotation>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationsLatestByIdQuery" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public StockQuotationsLatestByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}