using System;
using StockTradingAnalysis.Domain.Events.Domain;

namespace StockTradingAnalysis.Web.Migration.Entities
{
    public class StrategyDto : Strategy
    {
        public int OldId { get; set; }

        public StrategyDto()
            : base(Guid.NewGuid())
        {
        }
    }
}
