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

        public StrategyImageChangedEvent(Guid id, Type aggregateType, IImage image)
            : base(id, aggregateType)
        {
            Image = image;
        }
    }
}