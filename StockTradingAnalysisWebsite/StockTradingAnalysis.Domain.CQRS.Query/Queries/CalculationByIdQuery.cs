using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class CalculationByIdQuery : IQuery<ICalculation>
    {
        public Guid Id { get; private set; }

        public CalculationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}