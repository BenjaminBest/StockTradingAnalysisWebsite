namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines an interface for a dividend transaction book entry
    /// </summary>
    public interface IDividendTransactionBookEntry : ITransactionBookEntry
    {
        /// <summary>
        /// Gets the taxes paid
        /// </summary>
        decimal Taxes { get; }
    }
}