using StockTradingAnalysis.Interfaces.Domain;
using System.Collections.Generic;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The class Statistic defines time range based default statistical information without special statistic calculations
    /// </summary>
    public class Statistic : IStatistic
    {
        /// <summary>
        /// Absolute profit
        /// </summary>
        public decimal ProfitAbsolute { get; set; }

        /// <summary>
        /// Average profit, only profits will be counted
        /// </summary>
        public decimal ProfitAverage { get; set; }

        /// <summary>
        /// Maximum profit, only profits will be counted
        /// </summary>
        public decimal ProfitMaximum { get; set; }

        /// <summary>
        /// Average percentage profit per trade, only profits will be counted
        /// </summary>
        public decimal ProfitAveragePercentage { get; set; }

        /// <summary>
        /// Average loss, only losses will be counted
        /// </summary>
        public decimal LossAverage { get; set; }

        /// <summary>
        /// Maximum loss, only losses will be counted
        /// </summary>
        public decimal LossMaximum { get; set; }

        /// <summary>
        /// Average percentage loss per trade, only losses will be counted
        /// </summary>
        public decimal LossAveragePercentage { get; set; }

        /// <summary>
        /// Absolute loss, only losses will be counted
        /// </summary>
        public decimal LossAbsolute { get; set; }

        /// <summary>
        /// Average profit or loss
        /// </summary>
        public decimal TradeAverage { get; set; }

        /// <summary>
        /// Average percentage profit or loss
        /// </summary>
        public decimal TradeAveragePercentage { get; set; }

        /// <summary>
        /// Absolute profit or loss
        /// </summary>
        public decimal TradeAbsolute { get; set; }

        /// <summary>
        /// Amount of transactions
        /// </summary>
        public int AmountOfTransactions { get; set; }

        /// <summary>
        /// Amount of transactions per year
        /// </summary>
        public decimal AmountOfTransactionsPerYear { get; set; }

        /// <summary>
        /// Amount of transactions per month
        /// </summary>
        public decimal AmountOfTransactionsPerMonth { get; set; }

        /// <summary>
        /// Amount of transactions per week
        /// </summary>
        public decimal AmountOfTransactionsPerWeek { get; set; }

        /// <summary>
        /// Amount of loss transactions
        /// </summary>
        public int AmountOfLossTransactions { get; set; }

        /// <summary>
        /// Amount of profit transactions
        /// </summary>
        public int AmountOfProfitTransactions { get; set; }

        /// <summary>
        /// Percentage of loss transactions
        /// </summary>
        public decimal PercentageOfLossTransactions { get; set; }

        /// <summary>
        /// Percentage of profit transactions
        /// </summary>
        public decimal PercentageOfProfitTransactions { get; set; }

        /// <summary>
        /// Average volume which is used in transactions
        /// </summary>
        public decimal AverageBuyVolume { get; set; }

        /// <summary>
        /// Feedback Top 10 sorted by percentage
        /// </summary>
        public Dictionary<string, decimal> FeedbackTop5 { get; set; }

        /// <summary>
        /// All Feedback sorted by percentage
        /// </summary>
        public Dictionary<int, decimal> Feedback { get; set; }

        /// <summary>
        /// Absolute profit per asset class
        /// </summary>
        public Dictionary<string, decimal> AbsoluteProfitPerTradingType { get; set; }

        /// <summary>
        /// Absolute profit per stock name
        /// </summary>
        public Dictionary<string, decimal> AbsoluteProfitPerStockName { get; set; }

        /// <summary>
        /// Absolute profit per weekday
        /// </summary>
        public Dictionary<string, decimal> AbsoluteProfitPerWeekDay { get; set; }

        /// <summary>
        /// Average holding period for intraday trades
        /// </summary>
        /// <remarks>In minutes</remarks>
        public decimal AvgHoldingPeriodIntradayTrades { get; set; }

        /// <summary>
        /// Average holding period for position trades aka. trades over a few days
        /// </summary>
        /// <remarks>In days</remarks>
        public decimal AvgHoldingPeriodPositionTrades { get; set; }

        /// <summary>
        ///Amount of intraday trades
        /// </summary>
        /// <remarks>In minutes</remarks>
        public int AmountIntradayTrades { get; set; }

        /// <summary>
        /// Amount of position trades aka. trades over a few days
        /// </summary>
        /// <remarks>In days</remarks>
        public int AmountPositionTrades { get; set; }

        /// <summary>
        /// Sum of all order costs 
        /// </summary>
        public decimal SumOrderCosts { get; set; }

        /// <summary>
        /// Sum of all taxes paid
        /// </summary>
        public decimal SumTaxes { get; set; }

        /// <summary>
        /// Pay-Off-Ratio = Avg Profit/Avg Loss
        /// </summary>
        public decimal PayOffRatio { get; set; }

        /// <summary>
        /// Pay-Off-Ratio (Avg Profit/Avg Loss) Description
        /// </summary>
        public string PayOffRatioDescription { get; set; }

        /// <summary>
        /// Average CRV
        /// </summary>
        public decimal AverageCRV { get; set; }

        /// <summary>
        /// Best asset class name
        /// </summary>
        public string BestAssetClassName { get; set; }

        /// <summary>
        /// Best asset class profit
        /// </summary>
        public decimal BestAssetClassProfit { get; set; }

        /// <summary>
        /// Best asset name
        /// </summary>
        public string BestAssetName { get; set; }

        /// <summary>
        /// Best asset profit
        /// </summary>
        public decimal BestAssetProfit { get; set; }

        /// <summary>
        /// System Quality Number (SQN)
        /// </summary>
        /// <remarks>
        /// Root-square(amount of trades)*propability/stdev(R)
        /// </remarks>
        public decimal SQN { get; set; }

        /// <summary>
        /// Description of System Quality Number (SQN)
        /// </summary>
        public string SQNDescription { get; set; }

        /// <summary>
        /// Maximum losses in a row
        /// </summary>
        public int MostConsecutiveLosses { get; set; }

        /// <summary>
        /// Maximum wins in a row
        /// </summary>
        public int MostConsecutiveWins { get; set; }

        /// <summary>
        /// Maximum drawdown: Maximum loss cum. in a row
        /// </summary>
        public decimal MaxDrawdown { get; set; }

        /// <summary>
        /// Measures the average efficiency of entry in relation to the price range of the trade
        /// </summary>
        /// <remarks>i.e. how close was the entry in relation to the lowest low of the price range of the trade</remarks>
        public decimal AvgEntryEfficiency { get; set; }

        /// <summary>
        /// Measures the efficiency of the exit in relation to the price range of the trade
        /// </summary>
        /// <remarks>i.e. how close was the exit in relation to the highest high of the price range of the trade</remarks>
        public decimal AvgExitEfficiency { get; set; }

        /// <summary>
        /// Average MAE Absolute
        /// </summary>
        public decimal? AvgMAEAbsolute { get; set; }

        /// <summary>
        /// Average MFE Absolute
        /// </summary>
        public decimal? AvgMFEAbsolute { get; set; }
    }
}
