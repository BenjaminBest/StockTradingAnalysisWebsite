using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class TransactionCalculateDividendCommand : Command
    {
        public TransactionCalculateDividendCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}