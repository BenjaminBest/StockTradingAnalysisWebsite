using System;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class StrategyAddCommand : Command
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
        /// Initializes a new instance of the <see cref="StrategyAddCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="image">The image.</param>
        public StrategyAddCommand(
            Guid id,
            int version,
            string name,
            string description,
            IImage image)
            : base(version, id)
        {
            Name = name;
            Description = description;
            Image = image;
        }
    }
}