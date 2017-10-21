using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class CalculationRemoveCommand : Command
    {
        public CalculationRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}