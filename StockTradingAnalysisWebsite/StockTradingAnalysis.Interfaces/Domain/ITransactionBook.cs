using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface ITransactionBook defines a book to key track of all available transactions within a position
    /// identified by the stock id
    /// </summary>
    public interface ITransactionBook
    {
        /// <summary>
        /// Returns the open position
        /// </summary>
        /// <param name="stockId">The id of the stock</param>
        /// <returns>Open position</returns>
        IOpenPosition GetOrAddOpenPosition(Guid stockId);

        /// <summary>
        /// Returns all open positions
        /// </summary>
        /// <returns>Open positions</returns>
        IEnumerable<IOpenPosition> GetOpenPositions();

        /// <summary>
        /// Adds an entry to this bucket
        /// </summary>
        /// <param name="entry">The entry for a transaction</param>
        void AddEntry(ITransactionBookEntry entry);

        /// <summary>
        /// Returns all changes that were made by the commit (sell)
        /// </summary>
        /// <param name="stockId">The id of the stock</param>
        /// <returns>Book entries</returns>
        IEnumerable<ITransactionBookEntry> GetLastCommittedChanges(Guid stockId);
    }
}