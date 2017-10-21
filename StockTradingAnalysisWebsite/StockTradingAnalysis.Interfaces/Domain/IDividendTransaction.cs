namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for a dividend transaction
    /// </summary>
    public interface IDividendTransaction : ITransaction
    {
        /// <summary>
        /// Gets the taxes paid
        /// </summary>
        decimal Taxes { get; }
    }
}