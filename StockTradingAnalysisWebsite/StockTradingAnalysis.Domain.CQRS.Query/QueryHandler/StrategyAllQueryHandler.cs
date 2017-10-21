using System.Collections.Generic;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    public class StrategyAllQueryHandler : IQueryHandler<StrategyAllQuery, IEnumerable<IStrategy>>
    {
        private readonly IModelReaderRepository<IStrategy> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public StrategyAllQueryHandler(IModelReaderRepository<IStrategy> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<IStrategy> Execute(StrategyAllQuery query)
        {
            return _modelReaderRepository.GetAll();
        }
    }
}