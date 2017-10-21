using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class CalculationCopyCommand : Command
    {
        public Guid OriginalId { get; set; }

        public CalculationCopyCommand(Guid id, Guid originalId, int version)
            : base(version, id)
        {
            OriginalId = originalId;
        }
    }
}