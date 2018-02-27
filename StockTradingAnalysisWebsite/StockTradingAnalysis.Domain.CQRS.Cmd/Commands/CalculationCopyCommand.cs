using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class CalculationCopyCommand : Command
    {
        /// <summary>
        /// Gets or sets the original identifier.
        /// </summary>
        /// <value>
        /// The original identifier.
        /// </value>
        public Guid OriginalId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationCopyCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="originalId">The original identifier.</param>
        /// <param name="version">The version.</param>
        public CalculationCopyCommand(Guid id, Guid originalId, int version)
            : base(version, id)
        {
            OriginalId = originalId;
        }
    }
}