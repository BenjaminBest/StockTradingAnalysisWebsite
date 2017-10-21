using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StrategyByIdQuery : IQuery<IStrategy>
    {
        public Guid Id { get; private set; }

        public StrategyByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}