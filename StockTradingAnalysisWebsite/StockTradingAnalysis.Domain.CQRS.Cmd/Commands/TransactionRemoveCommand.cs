using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    /// <summary>
    /// The TransactionRemoveCommand is used when a transaction should be reverted.
    /// </summary>
    /// <seealso cref="Command" />
    public class TransactionRemoveCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRemoveCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public TransactionRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}