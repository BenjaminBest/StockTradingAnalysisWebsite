using System;

namespace StockTradingAnalysis.EventSourcing.Exceptions
{
    public class SnapshotNotSupportedException : Exception
    {
        public SnapshotNotSupportedException()
        {

        }

        public SnapshotNotSupportedException(string message)
            : base(message)
        {
        }
    }
}
