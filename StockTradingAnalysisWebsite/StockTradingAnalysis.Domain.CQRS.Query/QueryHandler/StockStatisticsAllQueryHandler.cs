using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The StockStatisticsAllQueryHandler returns a stock based statistics.
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{StockStatisticsAllQuery, IEnumerable{IStockStatistics}}" />
    public class StockStatisticsAllQueryHandler : IQueryHandler<StockStatisticsAllQuery, IEnumerable<IStockStatistics>>
    {
        /// <summary>
        /// The model reader repository
        /// </summary>
        private readonly IModelReaderRepository<IStockStatistics> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public StockStatisticsAllQueryHandler(IModelReaderRepository<IStockStatistics> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<IStockStatistics> Execute(StockStatisticsAllQuery query)
        {
            return _modelReaderRepository.GetAll().OrderByDescending(q => q.Performance);
        }
    }
}