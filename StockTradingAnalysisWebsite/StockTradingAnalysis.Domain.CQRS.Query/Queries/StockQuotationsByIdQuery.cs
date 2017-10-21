using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StockQuotationsByIdQuery : IQuery<IEnumerable<IQuotation>>
    {
        public Guid Id { get; private set; }

        public StockQuotationsByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}