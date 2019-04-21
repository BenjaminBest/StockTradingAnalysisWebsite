namespace StockTradingAnalysis.Web.Migration.Entities
{
    /// <summary>
    /// The old statistics will not be imported. It's used for testing purposes only.
    /// </summary>
    public class StatisticsDto
    {
        public int OldId { get; set; }

        public int OldTransactionEndId { get; set; }

        public decimal ProfitAbsolute { get; set; }

        public decimal ProfitPercentage { get; set; }

        public decimal R { get; set; }

        public decimal? EntryEfficiency { get; set; }

        public decimal? ExitEfficiency { get; set; }
    }
}
