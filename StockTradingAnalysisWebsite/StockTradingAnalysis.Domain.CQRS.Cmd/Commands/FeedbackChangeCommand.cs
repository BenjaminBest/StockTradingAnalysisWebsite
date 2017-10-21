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

        public FeedbackChangeCommand(Guid id, int version, string name, string description)
            : base(version, id)
        {
            Name = name;
            Description = description;
        }
    }
}