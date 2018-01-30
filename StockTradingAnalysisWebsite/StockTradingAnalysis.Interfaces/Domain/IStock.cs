using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for a stock
    /// </summary>
    public interface IStock : IVersionableModelRepositoryItem
    {
        /// <summary>
        /// Gets a combined display text which includes the name and wkn
        /// </summary>
        string StocksDescription { get; }

        /// <summary>
        /// Gets a combined display text which includes the name and wkn
        /// </summary>
        string StocksShortDescription { get; }

        /// <summary>
        /// Gets the name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the wkn
        /// </summary>
        string Wkn { get; }

        /// <summary>
        /// Gets the type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Gets if this stock is for buying or selling
        /// </summary>
        string LongShort { get; }

        /// <summary>
        /// List of quotations for this stock
        /// </summary>
        IEnumerable<IQuotation> Quotations { get; }
    }
}