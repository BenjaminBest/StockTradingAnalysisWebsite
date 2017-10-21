using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class FeedbackByIdQueryHandler : IQueryHandler<FeedbackByIdQuery, IFeedback>
    {
        private readonly IModelReaderRepository<IFeedback> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public FeedbackByIdQueryHandler(IModelReaderRepository<IFeedback> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IFeedback Execute(FeedbackByIdQuery query)
        {
            return _modelReaderRepository.GetById(query.Id);
        }
    }
}