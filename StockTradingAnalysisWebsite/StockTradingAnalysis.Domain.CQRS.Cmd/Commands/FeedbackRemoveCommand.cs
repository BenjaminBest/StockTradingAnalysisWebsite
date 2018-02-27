using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class FeedbackRemoveCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRemoveCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public FeedbackRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}