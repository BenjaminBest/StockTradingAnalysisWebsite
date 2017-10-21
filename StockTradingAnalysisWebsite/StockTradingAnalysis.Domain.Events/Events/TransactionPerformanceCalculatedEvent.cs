using System;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Types;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class TransactionPerformanceCalculatedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the absolute profit
        /// </summary>
        public decimal ProfitAbsolute { get; }

        /// <summary>
        /// Gets the profit (in %)
        /// </summary>
        public decimal ProfitPercentage { get; }

        /// <summary>
        /// Gets <c>true</c> if profit was made
        /// </summary>
        public bool ProfitMade { get; }

        /// <summary>
        /// Gets the holding period
        /// </summary>
        public HoldingPeriod HoldingPeriod { get; }

        /// <summary>
        /// Gets the maximum risk per trade
        /// </summary>
        public decimal R { get; }

        /// <summary>
        /// Gets the exit efficiency (based on MAE,MFE)
        /// </summary>
        public decimal? ExitEfficiency { get; }

        /// <summary>
        /// Gets the entry efficiency (based on MAE,MFE)
        /// </summary>
        public decimal? EntryEfficiency { get; }

        /// <summary>
        /// Gets the maximum loss during trade incl. order costs
        /// </summary>
        public decimal? MAEAbsolute { get; }

        /// <summary>
        /// Gets the maximum profit during trade incl. order costs
        /// </summary>
        public decimal? MFEAbsolute { get; }

        public TransactionPerformanceCalculatedEvent(Guid id, Type aggregateType,
            decimal profitAbsolute,
            decimal profitPercentage,
            bool profitMade,
            HoldingPeriod holdingPeriod,
            decimal r,
            decimal? exitEfficiency,
            decimal? entryEfficiency,
            decimal? maeAbsolute,
            decimal? mfeAbsolute)
            : base(id, aggregateType)
        {
            ProfitAbsolute = profitAbsolute;
            ProfitPercentage = profitPercentage;
            ProfitMade = profitMade;
            HoldingPeriod = holdingPeriod;
            R = r;
            ExitEfficiency = exitEfficiency;
            EntryEfficiency = entryEfficiency;
            MAEAbsolute = maeAbsolute;
            MFEAbsolute = mfeAbsolute;
        }
    }
}