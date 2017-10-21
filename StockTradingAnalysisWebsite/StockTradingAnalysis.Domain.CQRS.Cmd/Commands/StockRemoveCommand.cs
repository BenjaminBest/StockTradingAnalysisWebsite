using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class StockRemoveCommand : Command
    {
        public StockRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}