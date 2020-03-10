using System;
using StockTradingAnalysis.Domain.Events.Domain;

namespace StockTradingAnalysis.Web.Migration.Entities
{
    [Serializable]
    public class StockDto : Stock, ICloneable
    {
        public int OldId { get; set; }

        public bool IsDividend { get; set; }

        public StockDto(Guid aggregateId)
            : base(aggregateId)
        {
        }

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
