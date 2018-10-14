using System.Collections.Generic;
using System.Linq;
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
    public class StatisticsByTimeSliceQueryHandler : IQueryHandler<StatisticsByTimeSliceQuery, IEnumerable<IStatistic>>
    {
        private readonly ITimeSliceModelReaderRepository<IStatistic> _modelReaderRepository;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="modelReaderRepository">The model repository to read from</param>
        public StatisticsByTimeSliceQueryHandler(ITimeSliceModelReaderRepository<IStatistic> modelReaderRepository)
        {
            _modelReaderRepository = modelReaderRepository;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<IStatistic> Execute(StatisticsByTimeSliceQuery query)
        {
	        return query.TimeRange.GetAllSlices()
		        .Where(t => t.Type.Equals(query.TimeSliceFilter))
		        .Select(t => _modelReaderRepository.GetById(t))
		        .Where(statistic => statistic != null);
        }
    }
}