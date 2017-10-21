using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using System.Collections.Generic;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class OpenPositionsAllQueryHandler : IQueryHandler<OpenPositionsAllQuery, IEnumerable<IOpenPosition>>
    {
        private readonly ITransactionBook _transactionBook;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="transactionBook">The transaction book</param>
        public OpenPositionsAllQueryHandler(ITransactionBook transactionBook)
        {
            _transactionBook = transactionBook;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<IOpenPosition> Execute(OpenPositionsAllQuery query)
        {
            return _transactionBook.GetOpenPositions();
        }
    }
}