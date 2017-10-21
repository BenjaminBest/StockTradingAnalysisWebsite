namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines an interface for a selling transaction book entry
    /// </summary>
    public interface ISellingTransactionBookEntry : ITransactionBookEntry
    {
        /// <summary>
        /// Gets the taxes paid
        /// </summary>
        decimal Taxes { get; }
    }
}