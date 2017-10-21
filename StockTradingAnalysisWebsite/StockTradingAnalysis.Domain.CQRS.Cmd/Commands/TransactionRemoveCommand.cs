using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class TransactionRemoveCommand : Command
    {
        public TransactionRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}