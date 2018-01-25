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

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationStrikePriceChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="strikePrice">The strike price.</param>
        public CalculationStrikePriceChangedEvent(Guid id, Type aggregateType, decimal? strikePrice)
            : base(id, aggregateType)
        {
            StrikePrice = strikePrice;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationStrikePriceChangedEvent"/> class.
        /// </summary>
        protected CalculationStrikePriceChangedEvent()
        {

        }
    }
}