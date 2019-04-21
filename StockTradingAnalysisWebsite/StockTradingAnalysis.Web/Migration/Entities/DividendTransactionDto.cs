using System;
using StockTradingAnalysis.Domain.Events.Domain;

namespace StockTradingAnalysis.Web.Migration.Entities
{
    public class DividendTransactionDto : DividendTransaction, ITransactionDto
    {
        public int OldId { get; set; }

        public DividendTransactionDto()
            : base(Guid.NewGuid())
        {
        }
    }
}