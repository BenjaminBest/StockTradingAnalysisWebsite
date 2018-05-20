using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The query handler TransactionByStockIdQueryHandler returns all transactions by a given stock id and orders them by order date descending.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{TransactionByStockIdQuery, ITransaction}" />
    public class TransactionByStockIdQueryHandler : IQueryHandler<TransactionByStockIdQuery, IEnumerable<ITransaction>>
    {
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionByStockIdQueryHandler(IModelReaderRepository<ITransaction> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<ITransaction> Execute(TransactionByStockIdQuery query)
        {
            return _modelReaderRepository.GetAll().Where(t => t.Stock.Id.Equals(query.Id))
                .OrderByDescending(t => t.OrderDate);
        }
    }
}