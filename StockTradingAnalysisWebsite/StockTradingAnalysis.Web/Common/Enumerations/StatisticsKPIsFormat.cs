namespace StockTradingAnalysis.Web.Common.Enumerations
{
    /// <summary>
    /// Enumeration StatisticsKPIsFormat defines which symbol to use for kpi
    /// </summary>
    public enum StatisticsKPIsFormat
    {
        /// <summary>
        /// Values with no format or sign
        /// </summary>
        Absolute,

        /// <summary>
        /// Percentage values
        /// </summary>
        Percentage,

        /// <summary>
        /// Currency values
        /// </summary>
        Currency,

        /// <summary>
        /// Days Timeformat
        /// </summary>
        Days,

        /// <summary>
        /// Minutes Timeformat
        /// </summary>
        Minutes,

        /// <summary>
        /// Entry und Exit Efficiency
        /// </summary>
        Efficiency
    }
}