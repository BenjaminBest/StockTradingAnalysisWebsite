using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class TransactionByIdQuery : IQuery<ITransaction>
    {
        public Guid Id { get; private set; }

        public TransactionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}