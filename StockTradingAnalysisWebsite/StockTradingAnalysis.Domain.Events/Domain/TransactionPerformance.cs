using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Types;
using System;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    public class TransactionPerformance : ITransactionPerformance
    {
        /// <summary>
        /// Gets/sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the absolute profit
        /// </summary>
        public decimal ProfitAbsolute { get; set; }

        /// <summary>
        /// Gets the profit (in %)
        /// </summary>
        public decimal ProfitPercentage { get; set; }

        /// <summary>
        /// Gets <c>true</c> if profit was made
        /// </summary>
        public bool ProfitMade { get; set; }

        /// <summary>
        /// Gets the holding period
        /// </summary>
        public HoldingPeriod HoldingPeriod { get; set; }

        /// <summary>
        /// Gets the maximum risk per trade
        /// </summary>
        public decimal R { get; set; }

        /// <summary>
        /// Gets the exit efficiency (based on MAE,MFE)
        /// </summary>
        public decimal? ExitEfficiency { get; set; }

        /// <summary>
        /// Gets the entry efficiency (based on MAE,MFE)
        /// </summary>
        public decimal? EntryEfficiency { get; set; }

        /// <summary>
        /// Gets the maximum loss during trade incl. order costs
        /// </summary>
        public decimal? MAEAbsolute { get; set; }

        /// <summary>
        /// Gets the maximum profit during trade incl. order costs
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
