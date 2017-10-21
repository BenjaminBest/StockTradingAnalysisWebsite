namespace StockTradingAnalysis.Interfaces.Enumerations
{
    /// <summary>
    /// Defines the type of a performance counter
    /// </summary>
    public enum PerformanceType
    {
        /// <summary>
        /// Counts the total count of items
        /// </summary>
        NumberOfItems,
        /// <summary>
        /// Items per ms
        /// </summary>
        RateOfCountsPerMillisecond,
        /// <summary>
        /// Average time for a operation
        /// </summary>
        AverageTimer
    }
}