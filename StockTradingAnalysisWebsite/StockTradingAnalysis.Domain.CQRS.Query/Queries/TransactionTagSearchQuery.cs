using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class TransactionTagSearchQuery : IQuery<IEnumerable<string>>
    {
        public string SearchTerm { get; private set; }

        public TransactionTagSearchQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}