using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StockTypeSearchQuery : IQuery<IEnumerable<string>>
    {
        public string SearchTerm { get; private set; }

        public StockTypeSearchQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}