using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Enumerations;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    /// <summary>
    /// The query StatisticsByTimeSliceQuery returns all statistics for the given time range
    /// </summary>
    /// <seealso cref="Interfaces.Queries.IQuery{IEnumerable{IStatistic}}" />
    public class StatisticsByTimeSliceQuery : IQuery<IEnumerable<IStatistic>>
    {
        /// <summary>
        /// Gets the time range.
        /// </summary>
        /// <value>
        /// The time range.
        /// </value>
        public ITimeSlice TimeRange { get; }

		/// <summary>
		/// Gets or sets the time slice filter.
		/// </summary>
		/// <value>
		/// The time slice filter.
		/// </value>
		public TimeSliceType TimeSliceFilter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsByTimeSliceQuery"/> class.
        /// </summary>
        /// <param name="timeRange">The time range.</param>
        public StatisticsByTimeSliceQuery(ITimeSlice timeRange)
        {
            TimeRange = timeRange;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="StatisticsByTimeSliceQuery"/> class.
		/// </summary>
		/// <param name="timeRange">The time range.</param>
		/// <param name="timeSliceTypeFilter">The slice type which should be used to filter.</param>
		public StatisticsByTimeSliceQuery(ITimeSlice timeRange,TimeSliceType timeSliceTypeFilter)
	    {
		    TimeRange = timeRange;
		    TimeSliceFilter = timeSliceTypeFilter;

	    }
	}
}