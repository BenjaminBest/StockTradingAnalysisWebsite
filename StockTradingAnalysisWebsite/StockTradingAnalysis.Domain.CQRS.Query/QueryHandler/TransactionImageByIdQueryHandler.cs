using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class TransactionImageByIdQueryHandler : IQueryHandler<TransactionImageByIdQuery, IImage>
    {
        private readonly IModelReaderRepository<ITransaction> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public TransactionImageByIdQueryHandler(IModelReaderRepository<ITransaction> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IImage Execute(TransactionImageByIdQuery query)
        {
            return _modelReaderRepository.GetById(query.Id).Image;
        }
    }
}