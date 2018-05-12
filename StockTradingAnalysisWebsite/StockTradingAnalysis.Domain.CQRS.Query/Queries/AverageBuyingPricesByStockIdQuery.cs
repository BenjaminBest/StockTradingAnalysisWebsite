using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The query AverageBuyingPricesByStockIdQuery returns the average buying prices over time for the given stock.
    /// </summary>
    public class AverageBuyingPricesByStockIdQuery : IQuery<IEnumerable<IAverageBuyingPrice>>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsByStockIdQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public AverageBuyingPricesByStockIdQuery(Guid id)
        {
            Id = id;
        }
    }
}