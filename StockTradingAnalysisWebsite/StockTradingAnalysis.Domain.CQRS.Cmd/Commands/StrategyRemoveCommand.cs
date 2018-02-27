using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class StrategyRemoveCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyRemoveCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public StrategyRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}