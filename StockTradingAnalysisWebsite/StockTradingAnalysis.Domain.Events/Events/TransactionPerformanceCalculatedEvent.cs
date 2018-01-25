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
        public decimal ProfitAbsolute { get; private set; }

        /// <summary>
        /// Gets the profit (in %)
        /// </summary>
        public decimal ProfitPercentage { get; private set; }

        /// <summary>
        /// Gets <c>true</c> if profit was made
        /// </summary>
        public bool ProfitMade { get; private set; }

        /// <summary>
        /// Gets the holding period
        /// </summary>
        public HoldingPeriod HoldingPeriod { get; private set; }

        /// <summary>
        /// Gets the maximum risk per trade
        /// </summary>
        public decimal R { get; private set; }

        /// <summary>
        /// Gets the exit efficiency (based on MAE,MFE)
        /// </summary>
        public decimal? ExitEfficiency { get; private set; }

        /// <summary>
        /// Gets the entry efficiency (based on MAE,MFE)
        /// </summary>
        public decimal? EntryEfficiency { get; private set; }

        /// <summary>
        /// Gets the maximum loss during trade incl. order costs
        /// </summary>
        public decimal? MAEAbsolute { get; private set; }

        /// <summary>
        /// Gets the maximum profit during trade incl. order costs
        /// </summary>
        public decimal? MFEAbsolute { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionPerformanceCalculatedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="profitAbsolute">The profit absolute.</param>
        /// <param name="profitPercentage">The profit percentage.</param>
        /// <param name="profitMade">if set to <c>true</c> [profit made].</param>
        /// <param name="holdingPeriod">The holding period.</param>
        /// <param name="r">The r.</param>
        /// <param name="exitEfficiency">The exit efficiency.</param>
        /// <param name="entryEfficiency">The entry efficiency.</param>
        /// <param name="maeAbsolute">The mae absolute.</param>
        /// <param name="mfeAbsolute">The mfe absolute.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionPerformanceCalculatedEvent"/> class.
        /// </summary>
        protected TransactionPerformanceCalculatedEvent()
        {

        }
    }
}