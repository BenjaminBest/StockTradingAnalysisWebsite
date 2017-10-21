using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Data
{
    /// <summary>
    /// The interface IDatastore defines the interface for an document based database store
    /// </summary>
    public interface IDatastore<TItem>
    {
        /// <summary>
        /// Stores one item
        /// </summary>
        /// <typeparam name="TItem">The type of the item</typeparam>
        /// <param name="item">The item</param>
        void Store(TItem item);

        /// <summary>
        /// Stores a list of items
        /// </summary>
        /// <typeparam name="TItem">The type of the item</typeparam>
        /// <param name="items">The items</param>
        void Store(IEnumerable<TItem> items);

        /// <summary>
        /// Selects all items
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <returns>All items</returns>
        IEnumerable<TItem> Select();

        /// <summary>
        /// Selects all items with the given aggregateId
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <returns>All items</returns>
        IEnumerable<TItem> Select(Guid aggregateId);

        /// <summary>
        /// Deletes all documents
        /// </summary>
        void DeleteAll();
    }
}