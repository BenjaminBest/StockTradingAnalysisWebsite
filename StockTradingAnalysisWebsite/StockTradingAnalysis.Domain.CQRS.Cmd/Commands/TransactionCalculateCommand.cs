using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    /// <summary>
    /// The TransactionCalculateCommand is used when a closing transaction was executed and the performance should now be calculated.
    /// </summary>
    /// <seealso cref="Command" />
    public class TransactionCalculateCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCalculateCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public TransactionCalculateCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}