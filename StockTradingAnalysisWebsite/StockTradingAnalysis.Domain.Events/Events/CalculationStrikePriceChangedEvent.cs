using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class CalculationStrikePriceChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets/sets the strike price if selling
        /// </summary>
        public decimal? StrikePrice { get; private set; }

        public CalculationStrikePriceChangedEvent(Guid id, Type aggregateType, decimal? strikePrice)
            : base(id, aggregateType)
        {
            StrikePrice = strikePrice;
        }
    }
}