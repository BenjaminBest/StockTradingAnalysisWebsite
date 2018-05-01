using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IStatistic defines time range based default statistical information without special statistic calculations
    /// </summary>
    public interface IStatistic : ITimeSliceKey
    {
        /// <summary>
        /// Gets the time slice.
        /// </summary>
        /// <value>
        /// The time slice.
        /// </value>
        ITimeSlice TimeSlice { get; }

        /// <summary>
        /// Absolute profit
        /// </summary>
        decimal ProfitAbsolute { get; }

        /// <summary>
        /// Average profit, only profits will be counted
        /// </summary>
        decimal ProfitAverage { get; }

        /// <summary>
        /// Maximum profit, only profits will be counted
        /// </summary>
        decimal ProfitMaximum { get; }

        /// <summary>
        /// Average percentage profit per trade, only profits will be counted
        /// </summary>
        decimal ProfitAveragePercentage { get; }

        /// <summary>
        /// Average loss, only losses will be counted
        /// </summary>
        decimal LossAverage { get; }

        /// <summary>
        /// Maximum loss, only losses will be counted
        /// </summary>
        decimal LossMaximum { get; }

        /// <summary>
        /// Average percentage loss per trade, only losses will be counted
        /// </summary>
        decimal LossAveragePercentage { get; }

        /// <summary>
        /// Absolute loss, only losses will be counted
        /// </summary>
        decimal LossAbsolute { get; }

        /// <summary>
        /// Average profit or loss
        /// </summary>
        decimal TradeAverage { get; }

        /// <summary>
        /// Average percentage profit or loss
        /// </summary>
        decimal TradeAveragePercentage { get; }

        /// <summary>
        /// Absolute profit or loss
        /// </summary>
        decimal TradeAbsolute { get; }

        /// <summary>
        /// Amount of transactions
        /// </summary>
        int AmountOfTransactions { get; }

        /// <summary>
        /// Amount of transactions per year
        /// </summary>
        decimal AmountOfTransactionsPerYear { get; }

        /// <summary>
        /// Amount of transactions per month
        /// </summary>
        decimal AmountOfTransactionsPerMonth { get; }

        /// <summary>
        /// Amount of transactions per week
        /// </summary>
        decimal AmountOfTransactionsPerWeek { get; }

        /// <summary>
        /// Amount of loss transactions
        /// </summary>
        int AmountOfLossTransactions { get; }

        /// <summary>
        /// Amount of profit transactions
        /// </summary>
        int AmountOfProfitTransactions { get; }

        /// <summary>
        /// Percentage of loss transactions
        /// </summary>
        decimal PercentageOfLossTransactions { get; }

        /// <summary>
        /// Percentage of profit transactions
        /// </summary>
        decimal PercentageOfProfitTransactions { get; }

        /// <summary>
        /// Average volume which is used in transactions
        /// </summary>
        decimal AverageBuyVolume { get; }

        /// <summary>
        /// Feedback Top 10 sorted by percentage
        /// </summary>
        Dictionary<string, decimal> FeedbackTop5 { get; }

        /// <summary>
        /// Absolute profit per asset class
        /// </summary>
        Dictionary<string, decimal> AbsoluteProfitPerTradingType { get; }

        /// <summary>
        /// Absolute profit per stock name
        /// </summary>
        Dictionary<string, decimal> AbsoluteProfitPerStockName { get; }

        /// <summary>
        /// Absolute profit per weekday
        /// </summary>
        Dictionary<string, decimal> AbsoluteProfitPerWeekDay { get; }

        /// <summary>
        /// Average holding period for intraday trades
        /// </summary>
        /// <remarks>In minutes</remarks>
        decimal AvgHoldingPeriodIntradayTrades { get; }

        /// <summary>
        /// Average holding period for position trades aka. trades over a few days
        /// </summary>
        /// <remarks>In days</remarks>
        decimal AvgHoldingPeriodPositionTrades { get; }

        /// <summary>
        ///Amount of intraday trades
        /// </summary>
        /// <remarks>In minutes</remarks>
        int AmountIntradayTrades { get; }

        /// <summary>
        /// Amount of position trades aka. trades over a few days
        /// </summary>
        /// <remarks>In days</remarks>
        int AmountPositionTrades { get; }

        /// <summary>
        /// Sum of all order costs 
        /// </summary>
        decimal SumOrderCosts { get; }

        /// <summary>
        /// Sum of all taxes paid
        /// </summary>
        decimal SumTaxes { get; }

        /// <summary>
        /// Pay-Off-Ratio = Avg Profit/Avg Loss
        /// </summary>
        decimal PayOffRatio { get; }

        /// <summary>
        /// Pay-Off-Ratio (Avg Profit/Avg Loss) Description
        /// </summary>
        string PayOffRatioDescription { get; }

        /// <summary>
        /// Average CRV
        /// </summary>
        decimal AverageCRV { get; }

        /// <summary>
        /// Best asset class name
        /// </summary>
        string BestAssetClassName { get; }

        /// <summary>
        /// Best asset class profit
        /// </summary>
        decimal BestAssetClassProfit { get; }

        /// <summary>
        /// Best asset name
        /// </summary>
        string BestAssetName { get; }

        /// <summary>
        /// Best asset profit
        /// </summary>
        decimal BestAssetProfit { get; }

        /// <summary>
        /// System Quality Number (SQN)
        /// </summary>
        /// <remarks>
        /// Root-square(amount of trades)*propability/stdev(R)
        /// </remarks>
        decimal Sqn { get; }

        /// <summary>
        /// Description of System Quality Number (SQN)
        /// </summary>
        string SqnDescription { get; }

        /// <summary>
        /// Maximum losses in a row
        /// </summary>
        int MostConsecutiveLosses { get; }

        /// <summary>
        /// Maximum wins in a row
        /// </summary>
        int MostConsecutiveWins { get; }

        /// <summary>
        /// Maximum drawdown: Maximum loss cum. in a row
        /// </summary>
        decimal MaxDrawdown { get; }

        /// <summary>
        /// Measures the average efficiency of entry in relation to the price range of the trade
        /// </summary>
        /// <remarks>i.e. how close was the entry in relation to the lowest low of the price range of the trade</remarks>
        decimal AvgEntryEfficiency { get; }

        /// <summary>
        /// Measures the efficiency of the exit in relation to the price range of the trade
        /// </summary>
        /// <remarks>i.e. how close was the exit in relation to the highest high of the price range of the trade</remarks>
        decimal AvgExitEfficiency { get; }

        /// <summary>
        /// Average MAE Absolute
        /// </summary>
        decimal? AvgMAEAbsolute { get; }

        /// <summary>
        /// Average MFE Absolute
        /// </summary>
        decimal? AvgMFEAbsolute { get; }

        /// <summary>
        /// Information based on daily data, mainly used for charting
        /// </summary>
        //TODO:StatisticsCalcDaily DailyInformation

        /// <summary>
        /// Key performance indicators
        /// </summary>
        //TODO:IEnumerable<StatisticsKPI> KPI { get; } Create new service, inject this service

        /// <summary>
        /// Information based on daily data, mainly used for charting
        /// </summary>
        //TODO:StatisticsCalcDaily DailyInformation { get; }

        /// <summary>
        /// Loss clusters
        /// </summary>
        //TODO:List<StatisticsClusterNode> ClusterLosses { get; }

        /// <summary>
        /// Profit clusters
        /// </summary>
        //TODO:List<StatisticsClusterNode> ClusterProfits { get; }

        /// <summary>
        /// Profit and loss clusters combined
        /// </summary>
        //TODO:List<StatisticsClusterNode> ClusterCombined { get; }

        /// <summary>
        /// Profit and loss clusters combined, but clustered over R-multiples
        /// </summary>
        //TODO:List<StatisticsClusterNode> ClusterR { get; }

        /// <summary>
        /// Savings plan analysis
        /// </summary>
        //TODO:StatisticsAccumulationPlan AccumulationPlanAnalysis { get; }

        /// <summary>
        ///  Maximum Adverse Excursion (MAE)
        /// </summary>
        //TODO:List<StatisticsMAE> MAEDistribution { get; }

        /// <summary>
        ///  Maximum Favorable Excursion (MFE)
        /// </summary>
        //TODO:List<StatisticsMFE> MFEDistribution { get; }
    }

}