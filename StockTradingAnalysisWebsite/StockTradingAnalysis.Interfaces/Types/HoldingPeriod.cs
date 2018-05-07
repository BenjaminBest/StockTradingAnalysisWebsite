using System;

namespace StockTradingAnalysis.Interfaces.Types
{
    /// <summary>
    /// Defines the period of a trade
    /// </summary>
    public class HoldingPeriod
    {
        /// <summary>
        /// Gets if the trade was bought and sold at the same day
        /// </summary>
        public bool IsIntradayTrade { get; private set; }

        /// <summary>
        /// Gets the time span between the first buy and the sell
        /// </summary>
        public TimeSpan Period { get; private set; }

        /// <summary>
        /// Gets the start date of this period
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets the end date of this period
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="start">THe starting date</param>
        /// <param name="end">The enddate of this time span</param>
        public HoldingPeriod(DateTime start, DateTime end)
        {
            IsIntradayTrade = start.Date == end.Date;
            Period = end - start;
            StartDate = start;
            EndDate = end;
        }

        public bool HasValue()
        {
            return StartDate != default(DateTime) && EndDate != default(DateTime);
        }

        /// <summary>
        /// Returns a decimal representation of this period in minutes
        /// </summary>
        /// <returns></returns>
        public decimal ToMinutes()
        {
            return decimal.Round(Convert.ToDecimal(Period.TotalMinutes), 2);
        }

        /// <summary>
        /// Returns a decimal representation of this period in minutes
        /// </summary>
        /// <returns></returns>
        public decimal ToDays()
        {
            return decimal.Round(Convert.ToDecimal(Period.TotalDays), 2);
        }
    }
}
