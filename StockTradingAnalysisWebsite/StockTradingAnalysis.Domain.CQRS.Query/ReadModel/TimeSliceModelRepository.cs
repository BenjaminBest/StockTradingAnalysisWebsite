using System.Collections.Concurrent;
using System.Collections.Generic;
using StockTradingAnalysis.Domain.CQRS.Query.Exceptions;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.ReadModel
{
    /// <summary>
    /// The repository TimeSliceModelRepository stores time range based statistics.
    /// </summary>
    /// <seealso cref="ISupportsDataDeletion" />
    public class TimeSliceModelRepository<TItem> : ISupportsDataDeletion, ITimeSliceModelRepository<TItem> where TItem : ITimeSliceKey
    {
        /// <summary>
        /// The items
        /// </summary>
        protected readonly IDictionary<ITimeSliceKey, TItem> Items = new ConcurrentDictionary<ITimeSliceKey, TItem>();

        /// <summary>
        /// Returns the item with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>The item with the <paramref name="id"/> or <c>null</c></returns>
        public TItem GetById(ITimeSliceKey id)
        {
            return Items.TryGetValue(id, out var model) ? model : default(TItem);
        }

        /// <summary>
        /// Returns all items in this repository
        /// </summary>
        /// <returns>All items</returns>
        public IEnumerable<TItem> GetAll()
        {
            return Items.Values;
        }

        /// <summary>
        /// Updates the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be updated</param>
        /// <exception cref="ModelRepositoryUpdateException">Thrown if <paramref name="item"/> is null or has an empty id</exception>
        public void Update(TItem item)
        {
            if (item == null)
                throw new ModelRepositoryUpdateException("The given item was null");

            if (Items.ContainsKey(item))
            {
                Items[item] = item;
            }
            else
            {
                throw new ModelRepositoryAddException($"The given item cannot be updated because the id {item.Start.Date}-{item.End.Date} does not exist");
            }
        }

        /// <summary>
        /// Deletes the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be deleted</param>
        /// <exception cref="ModelRepositoryUpdateException">Thrown if <paramref name="item"/> is null or has an empty id</exception>
        public void Delete(TItem item)
        {
            if (item == null)
                throw new ModelRepositoryDeleteException("The given item was null");

            if (Items.ContainsKey(item))
            {
                Items.Remove(item);
            }
            else
            {
                throw new ModelRepositoryAddException($"The given item cannot be deleted because the id {item.Start.Date}-{item.End.Date} does not exist");
            }
        }

        /// <summary>
        /// Adds the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be added</param>
        /// <exception cref="ModelRepositoryAddException">Thrown if <paramref name="item"/> is null, has an empty id or already exists</exception>
        public void Add(TItem item)
        {
            if (item == null)
                throw new ModelRepositoryAddException("The given item was null");

            if (Items.ContainsKey(item))
                throw new ModelRepositoryAddException($"The given item with the id {item.Start.Date}-{item.End.Date} already exists");

            Items.Add(item, item);
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        public void DeleteAll()
        {
            Items.Clear();
        }
    }
}
