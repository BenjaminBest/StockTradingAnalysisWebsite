using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    /// <summary>
    /// The TransactionCalculateCommand is used when a dividend transaction was executed and the performance should now be calculated.
    /// </summary>
    /// <seealso cref="Command" />
    public class TransactionCalculateDividendCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCalculateDividendCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public TransactionCalculateDividendCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}