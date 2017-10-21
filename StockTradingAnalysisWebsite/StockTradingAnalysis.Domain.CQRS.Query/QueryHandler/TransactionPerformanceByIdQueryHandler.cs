using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class TransactionPerformanceByIdQueryHandler : IQueryHandler<TransactionPerformanceByIdQuery, ITransactionPerformance>
    {
        private readonly IModelReaderRepository<ITransactionPerformance> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionPerformanceByIdQueryHandler(IModelReaderRepository<ITransactionPerformance> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public ITransactionPerformance Execute(TransactionPerformanceByIdQuery query)
        {
            return _modelReaderRepository.GetById(query.Id);
        }
    }
}