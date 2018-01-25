using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class StrategyAddedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the image
        /// </summary>
        public IImage Image { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyAddedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="image">The image.</param>
        public StrategyAddedEvent(Guid id, Type aggregateType, string name, string description, IImage image)
            : base(id, aggregateType)
        {
            Name = name;
            Description = description;
            Image = image;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyAddedEvent"/> class.
        /// </summary>
        protected StrategyAddedEvent()
        {
            
        }
    }
}