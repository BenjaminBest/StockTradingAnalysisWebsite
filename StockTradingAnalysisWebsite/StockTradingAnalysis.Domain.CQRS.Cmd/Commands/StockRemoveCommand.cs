using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    /// <summary>
    /// The StockRemoveCommand is used when a stock should be deleted.
    /// </summary>
    /// <seealso cref="Command" />
    public class StockRemoveCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockRemoveCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public StockRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}