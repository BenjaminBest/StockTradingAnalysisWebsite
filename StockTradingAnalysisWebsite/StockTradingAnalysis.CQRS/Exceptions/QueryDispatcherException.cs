using System;

namespace StockTradingAnalysis.CQRS.Exceptions
{
    public class QueryDispatcherException : Exception
    {
        public Type HandlerType { get; private set; }

        public QueryDispatcherException(Type handlerType)
            : base(String.Format("The query handler of type '{0}' could not be retrieved", handlerType))
        {
            HandlerType = handlerType;
        }
    }
}