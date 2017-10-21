using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class TransactionPerformanceAllQuery : IQuery<IEnumerable<ITransactionPerformance>>
    {
    }
}