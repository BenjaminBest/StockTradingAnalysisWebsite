using System;
using StockTradingAnalysis.Domain.Events.Domain;

namespace StockTradingAnalysis.Web.Migration.Entities
{
    public class StockDto : Stock
    {
        public int OldId { get; set; }

        public bool IsDividend { get; set; }

        public StockDto()
            : base(Guid.NewGuid())
        {
        }
    }
}
