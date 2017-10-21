using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using System.Collections.Generic;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class FeedbackProportionAllQueryHandler : IQueryHandler<FeedbackProportionAllQuery, IEnumerable<IFeedbackProportion>>
    {
        private readonly IModelReaderRepository<IFeedbackProportion> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public FeedbackProportionAllQueryHandler(IModelReaderRepository<IFeedbackProportion> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<IFeedbackProportion> Execute(FeedbackProportionAllQuery query)
        {
            return _modelReaderRepository.GetAll();
        }
    }
}