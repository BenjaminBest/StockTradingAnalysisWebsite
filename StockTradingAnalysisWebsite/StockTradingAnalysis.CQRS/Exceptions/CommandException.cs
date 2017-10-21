using System;

namespace StockTradingAnalysis.CQRS.Exceptions
{
    public class CommandException : Exception
    {
        public CommandException(string message)
            : base(message)
        {
        }
    }
}