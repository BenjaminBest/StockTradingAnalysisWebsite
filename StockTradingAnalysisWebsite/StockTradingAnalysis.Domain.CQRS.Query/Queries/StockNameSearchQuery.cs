using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StockNameSearchQuery : IQuery<IEnumerable<string>>
    {
        public string SearchTerm { get; private set; }

        public StockNameSearchQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}