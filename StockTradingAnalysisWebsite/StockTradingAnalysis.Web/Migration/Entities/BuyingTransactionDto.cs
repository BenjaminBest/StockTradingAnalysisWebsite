using System;
using StockTradingAnalysis.Domain.Events.Domain;

namespace StockTradingAnalysis.Web.Migration.Entities
{
    [Serializable]
    public class BuyingTransactionDto : BuyingTransaction, ITransactionDto
    {
        public int OldId { get; set; }

        public BuyingTransactionDto()
            : base(Guid.NewGuid())
        {
        }
    }
}