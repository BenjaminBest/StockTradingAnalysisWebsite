using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Domain.CQRS.Query.Queries
{
    public class FeedbackByIdQuery : IQuery<IFeedback>
    {
        public Guid Id { get; private set; }

        public FeedbackByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}