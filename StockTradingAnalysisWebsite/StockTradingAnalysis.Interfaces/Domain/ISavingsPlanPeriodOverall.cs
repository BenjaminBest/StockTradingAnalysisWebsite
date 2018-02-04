namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ISavingsPlanPeriodOverall defines analysis information for saving plans for the overall time range
    /// </summary>
    public interface ISavingsPlanPeriodOverall
    {
        /// <summary>
        /// Get the sum of all inpayments
        /// </summary>
        decimal SumInpayment { get; }

        /// <summary>
        /// Get the sum of accumulated capital based on market values
        /// </summary>
        decimal SumCapital { get; }

        /// <summary>
        /// Get the sum of all order costs with negative sign
        /// </summary>
        decimal SumOrderCosts { get; }

        /// <summary>
        /// Get the percentage of ordercosts vs. inpayment
        /// </summary>
        decimal SumOrderCostsPercentage { get; }

        /// <summary>
        /// Gets or sets the performance overall period percentage.
        /// </summary>
        /// <value>
        /// The performance overall period percentage.
        /// </value>
        decimal PerformanceOverallPeriodPercentage { get; set; }

        /// <summary>
        /// Get the sum of dividend payments overall
        /// </summary>
        decimal SumOverallDividends { get; set; }

        /// <summary>
        /// Get the sum of dividend payments in percent overall
        /// </summary>
        decimal SumOverallDividendsPercentage { get; set; }
    }
}
