namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ISavingsPlanPeriod defines analysis information for saving plans
    /// </summary>
    public interface ISavingsPlanPeriod : ISavingsPlanPeriodCurrent, ISavingsPlanPeriodOverall
    {
        /// <summary>
        /// Gets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        string Year { get; }

        /// <summary>
        /// Forecast or historical data
        /// </summary>
        bool IsForecast { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is current year.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is current year; otherwise, <c>false</c>.
        /// </value>
        bool IsCurrentYear { get; }
    }
}
