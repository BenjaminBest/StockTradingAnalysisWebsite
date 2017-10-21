using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using System.Linq;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class StockQuotationsCountByIdQueryHandler : IQueryHandler<StockQuotationsCountByIdQuery, int>
    {
        private readonly IModelReaderRepository<IStock> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public StockQuotationsCountByIdQueryHandler(IModelReaderRepository<IStock> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public int Execute(StockQuotationsCountByIdQuery query)
        {
            return _modelReaderRepository.GetById(query.Id).Quotations.Count();
        }
    }
}