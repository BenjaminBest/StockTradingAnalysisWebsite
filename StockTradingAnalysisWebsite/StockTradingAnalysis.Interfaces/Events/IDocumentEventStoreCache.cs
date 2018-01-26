using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The IDocumentStoreCache is a cache for the document store to improve performance
    /// </summary>
    public interface IDocumentStoreCache<TType>
    {
        /// <summary>
        /// Adds the given <paramref name="item" /> to the internal cache
        /// </summary>
        /// <param name="item">The event</param>
        void Add(TType item);

        /// <summary>
        /// Adds the given <paramref name="item" /> to the internal cache
        /// </summary>
        /// <param name="item">A list of items</param>
        void Add(IEnumerable<TType> item);

        /// <summary>
        /// Returns all items which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>
        /// A list with all events
        /// </returns>
        IEnumerable<TType> Get(Guid aggregateId);
    }
}