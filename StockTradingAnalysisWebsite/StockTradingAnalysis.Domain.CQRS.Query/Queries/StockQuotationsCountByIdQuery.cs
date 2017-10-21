using StockTradingAnalysis.Interfaces.Queries;
using System;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StockQuotationsCountByIdQuery : IQuery<int>
    {
        public Guid Id { get; private set; }

        public StockQuotationsCountByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}