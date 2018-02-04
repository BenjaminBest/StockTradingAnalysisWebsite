using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The query StockQuotationsLastBeforeDateByIdQuery returns the last quotation of a given stock before a given date
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{IQuotation}" />
    public class StockQuotationsLastBeforeDateByIdQuery : IQuery<IQuotation>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationsLastBeforeDateByIdQuery" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="date">The date.</param>
        public StockQuotationsLastBeforeDateByIdQuery(Guid id, DateTime date)
        {
            Id = id;
            Date = date;
        }
    }
}