using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class StrategyRemoveCommand : Command
    {
        public StrategyRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}