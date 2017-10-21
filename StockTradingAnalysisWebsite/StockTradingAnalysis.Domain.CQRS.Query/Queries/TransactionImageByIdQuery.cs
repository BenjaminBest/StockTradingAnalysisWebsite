using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class TransactionImageByIdQuery : IQuery<IImage>
    {
        public Guid Id { get; private set; }

        public TransactionImageByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}