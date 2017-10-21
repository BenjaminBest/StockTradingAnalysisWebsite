using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IPersistentStore defines a persistent storage which is used to save or add items <typeparamref name="TItem"/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface IPersistentStore<TItem>
    {
        /// <summary>
        /// Adds the given <paramref name="item" /> to the internal storage
        /// </summary>
        /// <param name="item">The item to persist</param>
        void Add(TItem item);

        /// <summary>
        /// Adds the given <paramref name="items" /> to the internal storage
        /// </summary>
        /// <param name="items">The items to persist</param>
        void Add(IEnumerable<TItem> items);

        /// <summary>
        /// Returns all items from the internal storage
        /// </summary>
        /// <typeparam name="TItem">The type of an item</typeparam>
        /// <returns>A list with all items</returns>
        IEnumerable<TItem> All();

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all events</returns>
        IEnumerable<TItem> Find(Guid aggregateId);
    }
}