namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ISavingsPlanPeriodCurrent defines analysis information for saving plans for the current year
    /// </summary>
    public interface ISavingsPlanPeriodCurrent
    {
        /// <summary>
        /// Get the sum of dividend payments
        /// </summary>
        decimal SumDividends { get; set; }

        /// <summary>
        /// Gets or sets the performance actual period percentage.
        /// </summary>
        /// <value>
        /// The performance actual period percentage.
        /// </value>
        decimal PerformanceActualPeriodPercentage { get; set; }
    }
}
