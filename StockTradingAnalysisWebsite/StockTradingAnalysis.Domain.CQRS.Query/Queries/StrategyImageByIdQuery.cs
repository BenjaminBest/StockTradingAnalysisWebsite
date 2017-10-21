using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StrategyImageByIdQuery : IQuery<IImage>
    {
        public Guid Id { get; private set; }

        public StrategyImageByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}