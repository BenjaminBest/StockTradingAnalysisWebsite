using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.QueryHandler
{
    /// <summary>
    /// The StatisticByTimeSliceQueryHandler returns all statistic information
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQueryHandler{StatisticsByTimeSliceQuery, IEnumerable{IStatistic}}" />
    public class StatisticByTimeSliceQueryHandler : IQueryHandler<StatisticsByTimeSliceQuery, IStatistic>
    {
        private readonly ITimeSliceModelReaderRepository<IStatistic> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public StatisticByTimeSliceQueryHandler(ITimeSliceModelReaderRepository<IStatistic> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IStatistic Execute(StatisticsByTimeSliceQuery query)
        {
            return _modelReaderRepository.GetById(query.TimeRange);
        }
    }
}