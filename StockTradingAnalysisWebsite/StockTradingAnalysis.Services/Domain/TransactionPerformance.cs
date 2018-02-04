using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Types;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// Performance of one transaction
    /// </summary>
    /// <seealso cref="ITransactionPerformance" />
    public class TransactionPerformance : ITransactionPerformance
    {
        /// <summary>
        /// Gets/sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the absolute profit
        /// </summary>
        public decimal ProfitAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the profit (in %)
        /// </summary>
        public decimal ProfitPercentage { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if profit was made
        /// </summary>
        public bool ProfitMade { get; set; }

        /// <summary>
        /// Gets/sets the holding period
        /// </summary>
        public HoldingPeriod HoldingPeriod { get; set; }

        /// <summary>
        /// Gets/sets the maximum risk per trade
        /// </summary>
        public decimal R { get; set; }

        /// <summary>
        /// Gets/sets the exit efficiency (based on MAE,MFE)
        /// </summary>
        public decimal? ExitEfficiency { get; set; }

        /// <summary>
        /// Get/setss the entry efficiency (based on MAE,MFE)
        /// </summary>
        public decimal? EntryEfficiency { get; set; }

        /// <summary>
        /// Gets/sets the maximum loss during trade incl. order costs
        /// </summary>
        public decimal? MAEAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the maximum profit during trade incl. order costs
        /// </summary>
        public decimal? MFEAbsolute { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="tansactionId">The Id of the selling/dividend transaction</param>
        /// <param name="profitAbsolute">Absolute profit</param>
        /// <param name="profitPercentage">Relative profit</param>
        /// <param name="buyDate">The first buying date</param>
        /// <param name="sellDate">The sell date</param>
        /// <param name="r">The maximum risk per trade</param>
        public TransactionPerformance(Guid tansactionId, decimal profitAbsolute, decimal profitPercentage, DateTime buyDate, DateTime sellDate, decimal r)
        {
            Id = tansactionId;
            ProfitAbsolute = profitAbsolute;
            ProfitPercentage = profitPercentage;
            ProfitMade = ProfitAbsolute > 0;
            HoldingPeriod = new HoldingPeriod(buyDate, sellDate);
            R = r;
        }
    }
}
