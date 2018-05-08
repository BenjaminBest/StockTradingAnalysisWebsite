using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The class Statistic defines time range based default statistical information without special statistic calculations
    /// </summary>
    public class Statistic : IStatistic
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public DateTime Start => TimeSlice.Start;

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public DateTime End => TimeSlice.End;

        /// <summary>
        /// Gets the time slice.
        /// </summary>
        /// <value>
        /// The time slice.
        /// </value>
        public ITimeSlice TimeSlice { get; }

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
        public decimal Sqn { get; set; }

        /// <summary>
        /// Description of System Quality Number (SQN)
        /// </summary>
        public string SqnDescription { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="Statistic" /> class.
        /// </summary>
        /// <param name="slice">The slice.</param>
        public Statistic(ITimeSlice slice)
        {
            TimeSlice = slice;
            AbsoluteProfitPerWeekDay = new Dictionary<string, decimal>();
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <param name="other">An object to compare with this object.</param>
        protected bool Equals(Statistic other)
        {
            if (other == null)
                return false;

            return Start == other.Start && End == other.End;
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ITimeSliceKey other)
        {
            if (other == null)
                return false;

            return Start == other.Start && End == other.End;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((Statistic)obj);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Start.GetHashCode() * 397) ^ End.GetHashCode();
            }
        }
    }
}
