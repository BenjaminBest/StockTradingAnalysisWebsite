using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface ITimeSliceModelReaderRepository defines a repository which stores time range based statistics.
    /// </summary>
    public interface ITimeSliceModelReaderRepository<out TItem> where TItem : ITimeSliceKey
    {
        /// <summary>
        /// Returns the item with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>The item with the <paramref name="id"/> or <c>null</c></returns>
        TItem GetById(ITimeSliceKey id);

        /// <summary>
        /// Returns all items in this repository
        /// </summary>
        /// <returns>All items</returns>
        IEnumerable<TItem> GetAll();
    }
}