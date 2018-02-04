using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The query StockQuotationsLastOfYearByIdQuery returns the last quotation of a given year for a given stock.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{IQuotation}" />
    public class StockQuotationsLastOfYearByIdQuery : IQuery<IQuotation>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockQuotationsLastOfYearByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="year">The year.</param>
        public StockQuotationsLastOfYearByIdQuery(Guid id, int year)
        {
            Id = id;
            Year = year;
        }
    }
}