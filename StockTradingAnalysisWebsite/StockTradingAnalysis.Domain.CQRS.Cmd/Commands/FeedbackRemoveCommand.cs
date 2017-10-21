using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class FeedbackRemoveCommand : Command
    {
        public FeedbackRemoveCommand(Guid id, int version)
            : base(version, id)
        {
        }
    }
}