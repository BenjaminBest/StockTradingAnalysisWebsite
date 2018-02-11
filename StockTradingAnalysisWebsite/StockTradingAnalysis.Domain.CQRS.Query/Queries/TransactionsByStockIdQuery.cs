using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The query TransactionsByStockIdQuery returns all transactions for a given stock id and orders them by OrderDate descending.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{ITransaction}" />
    public class TransactionsByStockIdQuery : IQuery<IEnumerable<ITransaction>>
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
        public TransactionsByStockIdQuery(Guid id)
        {
            Id = id;
        }
    }
}