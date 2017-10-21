using System;
using StockTradingAnalysis.Domain.Events.Domain;

namespace StockTradingAnalysis.Web.Migration.Entities
{
    public class SellingTransactionDto : SellingTransaction, ITransactionDto
    {
        public int OldId { get; set; }

        public SellingTransactionDto()
            : base(Guid.NewGuid())
        {
        }
    }
}