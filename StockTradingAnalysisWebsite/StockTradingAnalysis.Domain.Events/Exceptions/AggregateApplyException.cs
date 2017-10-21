using System;

namespace StockTradingAnalysis.Domain.Events.Exceptions
{
    public class AggregateApplyException : Exception
    {
        public AggregateApplyException(string message)
            : base(message)
        {
        }
    }
}