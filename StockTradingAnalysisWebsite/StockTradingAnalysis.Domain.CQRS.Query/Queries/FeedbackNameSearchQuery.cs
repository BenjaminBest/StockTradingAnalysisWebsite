using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class FeedbackNameSearchQuery : IQuery<IEnumerable<string>>
    {
        public string SearchTerm { get; private set; }

        public FeedbackNameSearchQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}