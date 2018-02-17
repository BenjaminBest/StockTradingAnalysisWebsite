using System;
using System.Globalization;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Core.Services
{
    /// <summary>
    /// The service DateCalculationService is used to calculate time ranges and special dates based on given dates.
    /// </summary>
    public class DateCalculationService : IDateCalculationService
    {
        /// <summary>
        /// The epoch date time
        /// </summary>
        private readonly DateTime _epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Returns the start date of the week calculated with the given date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="end">End date</param>
        /// <returns>Start Date of Week</returns>
        public DateTime GetStartAndEndDateOfWeek(DateTime date, out DateTime end)
        {
            var start = date;

            while (start.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            {
                start = start.AddDays(-1);
            }

            end = start.AddDays(6);
            end = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);

            return start;
        }

        /// <summary>
        /// Returns the end date of the month calculated with the given date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="end">End date</param>
        /// <returns>Start Date of Month</returns>
        public DateTime GetStartAndEndDateOfMonth(DateTime date, out DateTime end)
        {
            var month = date.Month;
            var dayMax = DateTime.DaysInMonth(date.Year, month);

            var start = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
            end = new DateTime(date.Year, date.Month, dayMax, 23, 59, 59);

            return start;
        }

        /// <summary>
        /// Returns the end date of the month calculated with the given date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>End Date of Month</returns>
        public DateTime GetEndDateOfMonth(DateTime date)
        {
            var month = date.Month;
            var dayMax = DateTime.DaysInMonth(date.Year, month);

            return new DateTime(date.Year, date.Month, dayMax, 23, 59, 59);
        }

        /// <summary>
        /// Returns the end date of the week calculated with the given date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="end">End date</param>
        /// <returns>End Date of two weeks</returns>
        public DateTime GetStartAndEndDateOf2Weeks(DateTime date, out DateTime end)
        {
            end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            var start = end.AddDays(-14);
            start = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);

            return start;
        }

        /// <summary>
        /// Returns the end date of the the year
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="end">End date</param>
        /// <returns>Start Date of year</returns>
        public DateTime GetStartAndEndDateOfYear(DateTime date, out DateTime end)
        {
            var start = new DateTime(date.Year, 1, 1, 0, 0, 0);
            end = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, 12), 23, 59, 59);

            return start;
        }

        /// <summary>
        /// Returns the end date of the the year
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>End Date of year</returns>
        public DateTime GetEndDateOfYear(DateTime date)
        {
            return new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, 12), 23, 59, 59);
        }

        /// <summary>
        /// Returns the start date of the the year
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Start Date of year</returns>
        public DateTime GetStartDateOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Returns the end date of the the quarter
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="end">End date</param>
        /// <returns>End Date of quarter</returns>
        public DateTime GetStartAndEndDateOfQuarter(DateTime date, out DateTime end)
        {
            var currQuarter = (date.Month - 1) / 3 + 1;

            var start = new DateTime(date.Year, 3 * currQuarter - 2, 1, 0, 0, 0);

            if (currQuarter == 4)
            {
                end = new DateTime(date.Year + 1, 1, 1, 23, 59, 59).AddDays(-1);
            }
            else
            {
                end = new DateTime(date.Year, 3 * currQuarter + 1, 1, 23, 59, 59).AddDays(-1);
            }

            return start;
        }

        /// <summary>
        /// Returns the end date of the the quarter
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="end">End date</param>
        /// <param name="quarter">Quarter</param>
        /// <returns>End Date of quarter</returns>
        public DateTime GetStartAndEndDateOfQuarter(DateTime date, out DateTime end, out int quarter)
        {
            quarter = (date.Month - 1) / 3 + 1;

            var start = new DateTime(date.Year, 3 * quarter - 2, 1, 0, 0, 0);

            if (quarter == 4)
            {
                end = new DateTime(date.Year + 1, 1, 1, 23, 59, 59).AddDays(-1);
            }
            else
            {
                end = new DateTime(date.Year, 3 * quarter + 1, 1, 23, 59, 59).AddDays(-1);
            }

            return start;
        }

        /// <summary>
        /// Returns the calendar week of the given date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns></returns>
        public int GetCalendarWeek(DateTime date)
        {
            var culture = CultureInfo.CurrentCulture;
            return culture.Calendar.GetWeekOfYear(date, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Returns the start date of the given calendar week and the end date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DateTime FirstDateOfWeek(int year, int weekOfYear, out DateTime end)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            var firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);

            var start = result.AddDays(-3);
            start = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            end = start.AddDays(6);
            end = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);

            return start;
        }

        /// <summary>
        /// Returns the end of today
        /// </summary>
        /// <returns>End of Day</returns>
        public DateTime GetEndOfToDay()
        {
            var today = DateTime.Now;

            var result = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);

            return result;
        }

        /// <summary>
        /// Calculate the days between <paramref name="start"/> and <paramref name="end"/>
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>Amount of days</returns>
        public int DaysBetween(DateTime start, DateTime end)
        {
            return (int)(end - start).TotalDays;
        }

        /// <summary>
        /// Converts to epoch/posix time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Milliseconds since epoch</returns>
        public long ConvertToEpochTimeInMilliseconds(DateTime date)
        {
            var timeSpan = date - _epochDateTime;

            return (long)timeSpan.TotalMilliseconds;
        }
    }
}