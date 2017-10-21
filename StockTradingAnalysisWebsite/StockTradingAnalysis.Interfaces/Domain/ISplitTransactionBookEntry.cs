using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines an interface for a split/reverse split transaction book entry
    /// </summary>
    public interface ISplitTransactionBookEntry : ITransactionBookEntry
    {
        /// <summary>
        /// Create the new open position after the split
        /// </summary>        
        /// <param name="entries">The buying transactions before the split</param>
        /// <returns>Buying transaction book entry</returns>
        IBuyingTransactionBookEntry CreatePositionAfterSplit(IEnumerable<ITransactionBookEntry> entries);
    }
}