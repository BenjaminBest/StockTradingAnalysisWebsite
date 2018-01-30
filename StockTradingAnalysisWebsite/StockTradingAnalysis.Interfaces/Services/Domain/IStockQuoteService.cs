namespace StockTradingAnalysis.Interfaces.Services.Domain
{
    /// <summary>
    /// The interface IStockQuoteService defines a service to access current stock quotes
    /// </summary>
    public interface IStockQuoteService
    {
        /// <summary>
        /// Returns the quote of the given stock by <paramref name="wkn"/>
        /// </summary>
        /// <param name="wkn">The wkn of the stock</param>
        decimal GetQuote(string wkn);

        /// <summary>
        /// Returns the average true range of the given stock by <paramref name="wkn"/>
        /// </summary>
        /// <param name="wkn">The average true range (atr) of the stock</param>
        decimal GetAtr(string wkn);
    }
}