using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    public class AccumulationPlanStatistic : IAccumulationPlanStatistic
    {
        /// <summary>
        /// Get/sets the sum of all inpayments
        /// </summary>
        public decimal SumInpayment { get; set; }

        /// <summary>
        /// Get/sets the sum of accumulated capital based on market values
        /// </summary>
        public decimal SumCapitalEndofPeriod { get; set; }

        /// <summary>
        /// Get/sets the sum of accumulated capital based on market values
        /// </summary>
        public decimal SumCapital { get; set; }

        /// <summary>
        /// Get/sets the sum of accumulated capital based on todays market values
        /// </summary>
        public decimal SumCapitalToday { get; set; }

        /// <summary>
        /// Get/sets the sum of all order costs with negative sign
        /// </summary>
        public decimal SumOrderCosts { get; set; }

        /// <summary>
        /// Get/sets the percentage of ordercosts vs. inpayment
        /// </summary>
        public decimal SumOrderCostsPercentage { get; set; }

        /// <summary>
        /// Get/sets the performance calculated with the internal rate of interest.
        /// Money-weighted rate of return - MWR.
        /// </summary>
        public decimal PerformancePercentageIIR { get; set; }

        /// <summary>
        /// Get/sets the performance calculated with geometrical calculation
        /// Time-weighted rate of return - TWR.
        /// </summary>
        public decimal PerformancePercentageGeometrical { get; set; }

        /// <summary>
        /// Get/sets the sum of dividend payments
        /// </summary>
        public decimal SumDividends { get; set; }

        /// <summary>
        /// Get/sets the sum of dividend payments in percent
        /// </summary>
        public decimal SumDividendsPercentage { get; set; }
    }
}
