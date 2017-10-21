using StockTradingAnalysis.Interfaces.Filter;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ITransactionFilter defines a chainable filter-configuration for filtering a list of transactions
    /// </summary>
    public interface ITransactionFilter : IFilter<ITransaction>
    {
    }
}