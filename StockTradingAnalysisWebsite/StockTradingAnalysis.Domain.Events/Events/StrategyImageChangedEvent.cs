using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StrategyImageChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the image
        /// </summary>
        public IImage Image { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyImageChangedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="image">The image.</param>
        public StrategyImageChangedEvent(Guid id, Type aggregateType, IImage image)
            : base(id, aggregateType)
        {
            Image = image;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyImageChangedEvent"/> class.
        /// </summary>
        protected StrategyImageChangedEvent()
        {
            
        }
    }
}