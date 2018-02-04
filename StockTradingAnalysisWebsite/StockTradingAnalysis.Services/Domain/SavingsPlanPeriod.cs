using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The class SavingsPlanPeriod contains analysis information for a saving plan for a specific period
    /// </summary>
    public class SavingsPlanPeriod : ISavingsPlanPeriod
    {
        /// <summary>
        /// Gets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public string Year { get; set; }

        /// <summary>
        /// Get/sets the sum of all inpayments
        /// </summary>
        public decimal SumInpayment { get; set; }

        /// <summary>
        /// Get/sets the sum of accumulated capital based on market values
        /// </summary>
        public decimal SumCapital { get; set; }

        /// <summary>
        /// Get/sets the sum of all order costs with negative sign
        /// </summary>
        public decimal SumOrderCosts { get; set; }

        /// <summary>
        /// Get/sets the percentage of ordercosts vs. inpayment
        /// </summary>
        public decimal SumOrderCostsPercentage { get; set; }

        /// <summary>
        /// Gets or sets the performance actual period percentage.
        /// </summary>
        /// <value>
        /// The performance actual period percentage.
        /// </value>
        public decimal PerformanceActualPeriodPercentage { get; set; }

        /// <summary>
        /// Gets or sets the performance overall period percentage.
        /// </summary>
        /// <value>
        /// The performance overall period percentage.
        /// </value>
        public decimal PerformanceOverallPeriodPercentage { get; set; }

        /// <summary>
        /// Get the sum of dividend payments
        /// </summary>
        public decimal SumDividends { get; set; }

        /// <summary>
        /// Get/sets the sum of dividend payments
        /// </summary>
        public decimal SumOverallDividends { get; set; }

        /// <summary>
        /// Get/sets the sum of dividend payments in percent
        /// </summary>
        public decimal SumOverallDividendsPercentage { get; set; }

        /// <summary>
        /// Forecast or historical data
        /// </summary>
        public bool IsForecast { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is current year.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is current year; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrentYear { get; set; }
    }
}
