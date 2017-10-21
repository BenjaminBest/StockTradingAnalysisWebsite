namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IAccumulationPlanStatistic defines analysis information for saving plans
    /// </summary>
    public interface IAccumulationPlanStatistic
    {
        /// <summary>
        /// Get the sum of all inpayments
        /// </summary>
        decimal SumInpayment { get; }

        /// <summary>
        /// Get the sum of accumulated capital based on market values
        /// </summary>
        decimal SumCapitalEndofPeriod { get; }

        /// <summary>
        /// Get the sum of accumulated capital based on market values
        /// </summary>
        decimal SumCapital { get; }

        /// <summary>
        /// Get the sum of accumulated capital based on todays market values
        /// </summary>
        decimal SumCapitalToday { get; }

        /// <summary>
        /// Get the sum of all order costs with negative sign
        /// </summary>
        decimal SumOrderCosts { get; }

        /// <summary>
        /// Get the percentage of ordercosts vs. inpayment
        /// </summary>
        decimal SumOrderCostsPercentage { get; }

        /// <summary>
        /// Get the performance calculated with the internal rate of interest.
        /// Money-weighted rate of return - MWR.
        /// </summary>
        decimal PerformancePercentageIIR { get; }

        /// <summary>
        /// Get the performance calculated with geometrical calculation
        /// Time-weighted rate of return - TWR.
        /// </summary>
        decimal PerformancePercentageGeometrical { get; }

        /// <summary>
        /// Get the sum of dividend payments
        /// </summary>
        decimal SumDividends { get; set; }

        /// <summary>
        /// Get the sum of dividend payments in percent
        /// </summary>
        decimal SumDividendsPercentage { get; set; }
    }
}
