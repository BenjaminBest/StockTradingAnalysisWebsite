using System;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Types;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class TransactionDividendCalculatedEvent : DomainEvent
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

        public TransactionDividendCalculatedEvent(Guid id, Type aggregateType,
            decimal profitAbsolute,
            decimal profitPercentage,
            bool profitMade,
            HoldingPeriod holdingPeriod,
            decimal r)
            : base(id, aggregateType)
        {
            ProfitAbsolute = profitAbsolute;
            ProfitPercentage = profitPercentage;
            ProfitMade = profitMade;
            HoldingPeriod = holdingPeriod;
            R = r;
        }
    }
}