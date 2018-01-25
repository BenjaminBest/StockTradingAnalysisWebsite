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
        /// Initializes a new instance of the <see cref="TransactionDividendCalculatedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="profitAbsolute">The profit absolute.</param>
        /// <param name="profitPercentage">The profit percentage.</param>
        /// <param name="profitMade">if set to <c>true</c> [profit made].</param>
        /// <param name="holdingPeriod">The holding period.</param>
        /// <param name="r">The r.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDividendCalculatedEvent"/> class.
        /// </summary>
        protected TransactionDividendCalculatedEvent()
        {

        }
    }
}