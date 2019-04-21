using System;
using StockTradingAnalysis.Domain.Events.Domain;

namespace StockTradingAnalysis.Web.Migration.Entities
{
    public class CalculationDto : Calculation
    {
        public int OldId { get; set; }

        public CalculationDto()
            : base(Guid.NewGuid())
        {
        }
    }
}
