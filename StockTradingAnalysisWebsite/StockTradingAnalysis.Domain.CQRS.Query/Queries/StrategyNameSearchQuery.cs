using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StrategyNameSearchQuery : IQuery<IEnumerable<string>>
    {
        public string SearchTerm { get; private set; }

        public StrategyNameSearchQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}