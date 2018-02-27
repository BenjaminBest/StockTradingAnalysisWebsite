using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class FeedbackChangeCommand : Command
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
        /// Initializes a new instance of the <see cref="FeedbackChangeCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public FeedbackChangeCommand(Guid id, int version, string name, string description)
            : base(version, id)
        {
            Name = name;
            Description = description;
        }
    }
}