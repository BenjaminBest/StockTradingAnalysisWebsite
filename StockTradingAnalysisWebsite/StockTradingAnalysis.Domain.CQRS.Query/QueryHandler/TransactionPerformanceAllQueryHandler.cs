using System.Collections.Generic;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The TransactionPerformanceAllQueryHandler returns all transaction performances.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{TransactionPerformanceAllQuery, IEnumerable{ITransactionPerformance}}" />
    public class TransactionPerformanceAllQueryHandler : IQueryHandler<TransactionPerformanceAllQuery, IEnumerable<ITransactionPerformance>>
    {
        /// <summary>
        /// The model reader repository
        /// </summary>
        private readonly IModelReaderRepository<ITransactionPerformance> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionPerformanceAllQueryHandler(IModelReaderRepository<ITransactionPerformance> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<ITransactionPerformance> Execute(TransactionPerformanceAllQuery query)
        {
            return _modelReaderRepository.GetAll();
        }
    }
}