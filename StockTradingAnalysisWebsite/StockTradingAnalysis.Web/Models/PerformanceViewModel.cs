using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The PerformanceViewModel contains the information in regards to earnings in relation to a specific time frame.
    /// </summary>
    public class PerformanceViewModel
    {
        /// <summary>
        /// Gets or sets the time range.
        /// </summary>
        /// <value>
        /// The time range.
        /// </value>
        public string TimeRange { get; set; }

        /// <summary>
        /// Gets or sets the profit.
        /// </summary>
        /// <value>
        /// The profit.
        /// </value>
        [Display(Name = "Display_PerformanceProfit", ResourceType = typeof(Resources))]
        [UIHint("Profit")]
        public ProfitViewModel Profit { get; set; }

        /// <summary>
        /// Gets or sets the expected trade average, which is the average profit or loss per trade.
        /// </summary>
        /// <value>
        /// The expected trade average.
        /// </value>
        [Display(Name = "Display_PerformanceExpectedTradeAverage", ResourceType = typeof(Resources))]
        [UIHint("Profit")]
        public ProfitViewModel TradeAverage { get; set; }

        /// <summary>
        /// Amount of transactions
        /// </summary>
        [Display(Name = "Display_PerformanceAmountOfTransactions", ResourceType = typeof(Resources))]
        public int AmountOfTransactions { get; set; }

        /// <summary>
        /// Amount of loss transactions
        /// </summary>
        [Display(Name = "Display_PerformanceAmountOfLossTransactions", ResourceType = typeof(Resources))]
        public int AmountOfLossTransactions { get; set; }

        /// <summary>
        /// Amount of profit transactions
        /// </summary>
        [Display(Name = "Display_PerformanceAmountOfProfitTransactions", ResourceType = typeof(Resources))]
        public int AmountOfProfitTransactions { get; set; }

        /// <summary>
        /// Percentage of loss transactions
        /// </summary>
        [UIHint("PerformanceRedPercentage")]
        [Display(Name = "Display_PerformancePercentageOfLossTransactions", ResourceType = typeof(Resources))]
        public decimal PercentageOfLossTransactions { get; set; }

        /// <summary>
        /// Percentage of profit transactions
        /// </summary>
        [UIHint("PerformanceGreenPercentage")]
        [Display(Name = "Display_PerformancePercentageOfProfitTransactions", ResourceType = typeof(Resources))]
        public decimal PercentageOfProfitTransactions { get; set; }


        /// <summary>
        /// Pay-Off-Ratio = AVG Profit / AVG Loss
        /// </summary>
        [Display(Name = "Display_PerformancePayOffRatio", ResourceType = typeof(Resources))]
        [UIHint("PayOffRatio")]
        public PayOffRatioViewModel PayOffRatio { get; set; }

        /// <summary>
        /// System Quality Number
        /// </summary>
        /// <remarks>
        /// Sqrt(Tradeamount)*erw/stdev(R)
        /// </remarks>
        [Display(Name = "Display_PerformanceSQN", ResourceType = typeof(Resources))]
        [UIHint("Sqn")]
        public SqnViewModel Sqn { get; set; }

        /// <summary>
        /// Average holding period for intraday trades
        /// </summary>
        /// <remarks>In minutes</remarks>
        [Display(Name = "Display_PerformanceAvgHoldingPeriodIntradayTrades", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F0}")]
        public decimal AvgHoldingPeriodIntradayTrades { get; set; }

        /// <summary>
        /// Average holding period for position trades aka. trades over a few days
        /// </summary>
        /// <remarks>In days</remarks>
        [Display(Name = "Display_PerformanceAvgHoldingPeriodPositionTrades", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F0}")]
        public decimal AvgHoldingPeriodPositionTrades { get; set; }

        /// <summary>
        /// Average volume which is used in transactions
        /// </summary>
        [UIHint("PerformanceAbsolute")]
        [Display(Name = "Display_PerformanceAverageBuyVolume", ResourceType = typeof(Resources))]
        public decimal AverageBuyingVolume { get; set; }
    }
}