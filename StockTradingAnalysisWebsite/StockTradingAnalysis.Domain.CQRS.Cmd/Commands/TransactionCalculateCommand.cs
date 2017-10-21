using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class TransactionCalculateCommand : Command
    {
        public TransactionCalculateCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}