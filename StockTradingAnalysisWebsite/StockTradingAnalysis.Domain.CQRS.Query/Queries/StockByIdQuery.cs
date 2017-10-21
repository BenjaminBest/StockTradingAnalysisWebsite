using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StockByIdQuery : IQuery<IStock>
    {
        public Guid Id { get; private set; }

        public StockByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}