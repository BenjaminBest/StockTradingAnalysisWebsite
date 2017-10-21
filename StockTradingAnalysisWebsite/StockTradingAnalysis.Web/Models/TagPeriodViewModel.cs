using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// Class ViewModelTagStatistic is used for showing historical information for one period (year)
    /// </summary>
    public class TagPeriodViewModel
    {
        /// <summary>
        /// Year
        /// </summary>
        [Display(Name = "Display_TagPeriodYear", ResourceType = typeof(Resources))]
        [DisplayFormat(NullDisplayText = "")]
        public string Year
        {
            get;
            set;
        }

        /// <summary>
        /// Sum of all inpayments
        /// </summary>
        [UIHint("PerformanceAbsolute")]
        [Display(Name = "Display_TagPeriodSumInpayment", ResourceType = typeof(Resources))]
        public decimal SumInpayment { get; set; }

        /// <summary>
        /// Sum of accumulated capital based on market values
        /// </summary>
        [UIHint("PerformanceAbsolute")]
        [Display(Name = "Display_TagPeriodSumCapital", ResourceType = typeof(Resources))]
        public decimal SumCapital { get; set; }

        /// <summary>
        /// Sum of all order costs
        /// </summary>
        [UIHint("PerformanceRedAbsolute")]
        [Display(Name = "Display_TagPeriodSumOrderCosts", ResourceType = typeof(Resources))]
        public decimal SumOrderCosts { get; set; }

        /// <summary>
        /// Percentage of ordercosts vs. inpayment
        /// </summary>
        [UIHint("PerformanceRedPercentage")]
        [Display(Name = "Display_TagPeriodSumOrderCosts", ResourceType = typeof(Resources))]
        public decimal SumOrderCostsPercentage { get; set; }

        /// <summary>
        /// Performance per period money wheighted
        /// </summary>
        [UIHint("PerformancePercentage")]
        [Display(Name = "Display_TagPerformanceActualPeriodPercentage", ResourceType = typeof(Resources))]
        public decimal PerformanceActualPeriodPercentage { get; set; }

        /// <summary>s
        /// Overall performance money wheighted
        /// </summary>
        [UIHint("PerformancePercentage")]
        [Display(Name = "Display_TagPerformanceOverallPeriodPercentage", ResourceType = typeof(Resources))]
        public decimal PerformanceOverallPeriodPercentage { get; set; }

        /// <summary>
        /// Performance per period time wheighted
        /// </summary>
        [UIHint("PerformancePercentage")]
        [Display(Name = "Display_TagPerformanceActualPeriodPercentage", ResourceType = typeof(Resources))]
        public decimal PerformanceActualPeriodPercentageTW { get; set; }

        /// <summary>
        /// Overall performance time wheighted
        /// </summary>
        [UIHint("PerformancePercentage")]
        [Display(Name = "Display_TagPerformanceOverallPeriodPercentage", ResourceType = typeof(Resources))]
        public decimal PerformanceOverallPeriodPercentageTW { get; set; }

        /// <summary>
        /// Sum of dividend payments
        /// </summary>
        [UIHint("PerformanceAbsolute")]
        [Display(Name = "Display_TagDividends", ResourceType = typeof(Resources))]
        public decimal Dividends { get; set; }

        /// <summary>
        /// Sum of dividend payments in percent
        /// </summary>
        [UIHint("PerformancePercentage")]
        [Display(Name = "Display_TagDividendsPercentage", ResourceType = typeof(Resources))]
        public decimal DividendsPercentage { get; set; }

        /// <summary>
        /// Forecast or historical data
        /// </summary>
        public bool IsForecast
        {
            get;
            set;
        }
    }
}