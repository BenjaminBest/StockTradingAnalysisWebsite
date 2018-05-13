using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.External.Interfaces
{
    /// <summary>
    /// The interface IStockQuoteExternalService defines a sevice which connects to external resources and downloads quotes.
    /// </summary>
    public interface IStockQuoteExternalService
    {
        /// <summary>
        /// Gets the quotes for the specified WKN.
        /// </summary>
        /// <param name="wkn">The WKN.</param>
        /// <returns></returns>
        IEnumerable<IQuotation> Get(string wkn);

        /// <summary>
        /// Determines whether this instance is online.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
        /// </returns>
        bool IsOnline();
    }
}