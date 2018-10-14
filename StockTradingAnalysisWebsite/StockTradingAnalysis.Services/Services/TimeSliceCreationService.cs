using System;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Services
{
    /// <summary>
    /// The TimeSliceCreationService creates all time slices for all executed transactions or the slices which might have changed data because some
    /// change on a date affects them.
    /// </summary>
    public class TimeSliceCreationService : ITimeSliceCreationService
    {
        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// The date calculation service
        /// </summary>
        private readonly IDateCalculationService _dateCalculationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSliceCreationService" /> class.
        /// </summary>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        /// <param name="dateCalculationService">The date calculation service.</param>
        public TimeSliceCreationService(IQueryDispatcher queryDispatcher, IDateCalculationService dateCalculationService)
        {
            _queryDispatcher = queryDispatcher;
            _dateCalculationService = dateCalculationService;
        }

        /// <summary>
        /// Creates all time slices.
        /// </summary>
        /// <returns></returns>
        public ITimeSlice CreateTimeSlices()
        {
            return CreateTimeSlices(DateTime.MinValue);
        }

        /// <summary>
        /// Creates the time slices which are affected by the given <paramref name="date"/>
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public ITimeSlice CreateTimeSlices(DateTime date)
        {
            var minOrderDate = _queryDispatcher.Execute(new TransactionMinimumOrderDateQuery());
            var maxOrderDate = _queryDispatcher.Execute(new TransactionMaximumOrderDateQuery());

            if (minOrderDate == DateTime.MinValue || maxOrderDate == DateTime.MaxValue)
                return new AllTimeSlice(DateTime.MinValue, DateTime.MaxValue);

            var allSlice = new AllTimeSlice(_dateCalculationService.GetStartDateOfYear(minOrderDate), _dateCalculationService.GetEndDateOfYear(maxOrderDate));

            CreateYears(allSlice, date);

            return allSlice;
        }

        /// <summary>
        /// Creates the years.
        /// </summary>
        /// <param name="slice">The slice.</param>
        /// <param name="date">The minimum date. Any date earlier that this will not be incorporated if not affected.</param>
        //TODO: Add option to skip quarters and months for performance when used for example on performance dashboard
        private void CreateYears(AllTimeSlice slice, DateTime date)
        {
            foreach (var year in _dateCalculationService.GetInvolvedYears(slice.Start, slice.End))
            {
                //Skip earlier years
                if (year < date.Year)
                    continue;

                var startOfYear = _dateCalculationService.GetStartAndEndDateOfYear(year, out DateTime endOfYear);

                var yearSlice = new YearTimeSlice(startOfYear, endOfYear, year);
                CreateQuarters(yearSlice, date);
                slice.AddSlice(yearSlice);
            }
        }

        /// <summary>
        /// Creates the quarters.
        /// </summary>
        /// <param name="slice">The slice.</param>
        /// <param name="date">The minimum date. Any date earlier that this will not be incorporated if not affected.</param>
        private void CreateQuarters(YearTimeSlice slice, DateTime date)
        {
            var affectedQuarter = _dateCalculationService.GetQuarterOfDate(date);

            for (var i = 0; i <= 3; i++)
            {
                var month = (i * 3) + 1;

                var startOfQuarter =
                    _dateCalculationService.GetStartAndEndDateOfQuarter(new DateTime(slice.Start.Year, month, 1), out DateTime endOfQuater, out int quarter);

                if (slice.Start.Year != date.Year || slice.Start.Year == date.Year && quarter >= affectedQuarter)
                {
                    var quarterSlice = new QuarterTimeSlice(startOfQuarter, endOfQuater, quarter);
                    //Note: Not necessary now: CreateMonths(quarterSlice, date);
                    slice.AddSlice(quarterSlice);
                }
            }
        }

        /// <summary>
        /// Creates the months.
        /// </summary>
        /// <param name="slice">The slice.</param>
        /// <param name="date">The minimum date. Any date earlier that this will not be incorporated if not affected.</param>
        private void CreateMonths(QuarterTimeSlice slice, DateTime date)
        {
            foreach (var month in _dateCalculationService.GetMonthsInQuarter(slice.Value))
            {
                var startOfMonth = _dateCalculationService.GetStartAndEndDateOfMonth(new DateTime(slice.Start.Year, month, 1), out DateTime endOfMonth);

                if (slice.Start.Year != date.Year || slice.Start.Year == date.Year && month >= date.Month)
                {
                    slice.AddSlice(new MonthTimeSlice(startOfMonth, endOfMonth, month));
                }
            }
        }
    }
}
