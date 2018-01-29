using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class StockTypeInUseSearchQuery : IQuery<IEnumerable<string>>
    {
        public string SearchTerm { get; }

        public StockTypeInUseSearchQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}