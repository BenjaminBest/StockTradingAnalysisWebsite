using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface IModelReaderRepository{TModel} defines an repository for read access
    /// </summary>
    /// <typeparam name="TItem">The underlying domain model</typeparam>
    public interface IModelReaderRepository<out TItem> where TItem : class, IModelRepositoryItem
    {
        /// <summary>
        /// Returns the item with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>The item with the <paramref name="id"/></returns>
        TItem GetById(Guid id);

        /// <summary>
        /// Returns all items in this repository
        /// </summary>
        /// <returns>All items</returns>
        IEnumerable<TItem> GetAll();
    }
}