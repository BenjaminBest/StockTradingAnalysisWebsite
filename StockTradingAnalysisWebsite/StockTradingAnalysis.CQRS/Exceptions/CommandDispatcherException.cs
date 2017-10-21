using System;

namespace StockTradingAnalysis.CQRS.Exceptions
{
    public class CommandDispatcherException : Exception
    {
        public Type HandlerType { get; private set; }

        public CommandDispatcherException(Type handlerType)
            : base(String.Format("The command handler of type '{0}' could not be retrieved", handlerType))
        {
            HandlerType = handlerType;
        }
    }
}