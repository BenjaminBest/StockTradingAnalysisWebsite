using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class TransactionPerformanceByIdQuery : IQuery<ITransactionPerformance>
    {
        public Guid Id { get; private set; }

        public TransactionPerformanceByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}