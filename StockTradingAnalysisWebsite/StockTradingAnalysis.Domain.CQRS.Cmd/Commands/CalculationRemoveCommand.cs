using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class CalculationRemoveCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationRemoveCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public CalculationRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}