using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class FeedbackAllQueryHandler : IQueryHandler<FeedbackAllQuery, IEnumerable<IFeedback>>
    {
        private readonly IModelReaderRepository<IFeedback> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public FeedbackAllQueryHandler(IModelReaderRepository<IFeedback> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<IFeedback> Execute(FeedbackAllQuery query)
        {
            return _modelReaderRepository.GetAll().OrderBy(q => q.Name);
        }
    }
}