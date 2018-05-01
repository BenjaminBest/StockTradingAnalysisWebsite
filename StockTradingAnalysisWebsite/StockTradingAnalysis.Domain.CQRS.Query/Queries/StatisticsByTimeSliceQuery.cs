using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The query StatisticsByTimeSliceQuery returns all statistics for the given time range
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{IEnumerable{IStatistic}}" />
    public class StatisticsByTimeSliceQuery : IQuery<IStatistic>
    {
        /// <summary>
        /// Gets the time range.
        /// </summary>
        /// <value>
        /// The time range.
        /// </value>
        public ITimeSlice TimeRange { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsByTimeSliceQuery"/> class.
        /// </summary>
        /// <param name="timeRange">The time range.</param>
        public StatisticsByTimeSliceQuery(ITimeSlice timeRange)
        {
            TimeRange = timeRange;
        }
    }
}