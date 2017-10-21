using System;
using System.Collections.Generic;
using StockTradingAnalysis.Domain.CQRS.Query.Exceptions;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.CQRS.Query.ReadModel
{
    /// <summary>
    /// The repository for stocks
    /// </summary>
    public class ModelRepositoryBase<TItem> : IModelRepository<TItem> where TItem : class, IModelRepositoryItem
    {
        protected readonly IDictionary<Guid, TItem> _items = new Dictionary<Guid, TItem>();

        /// <summary>
        /// Returns the item with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>The item with the <paramref name="id"/> or <c>null</c></returns>
        public TItem GetById(Guid id)
        {
            TItem model;

            return _items.TryGetValue(id, out model) ? model : null;
        }

        /// <summary>
        /// Returns all items in this repository
        /// </summary>
        /// <returns>All items</returns>
        public IEnumerable<TItem> GetAll()
        {
            return _items.Values;
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

            if (item.Id == Guid.Empty)
                throw new ModelRepositoryUpdateException("The given item has an empty id");

            if (_items.ContainsKey(item.Id))
            {
                _items[item.Id] = item;
            }
            else
            {
                throw new ModelRepositoryAddException($"The given item cannot be updated because the id '{item.Id}' does not exist");
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

            if (item.Id == Guid.Empty)
                throw new ModelRepositoryDeleteException("The given item has an empty id");

            if (_items.ContainsKey(item.Id))
            {
                _items.Remove(item.Id);
            }
            else
            {
                throw new ModelRepositoryAddException($"The given item cannot be deleted because the id '{item.Id}' does not exist");
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

            if (item.Id == Guid.Empty)
                throw new ModelRepositoryAddException("The given item has an empty id");

            if (_items.ContainsKey(item.Id))
                throw new ModelRepositoryAddException($"The given item with the id '{item.Id}' already exists");

            _items.Add(item.Id, item);
        }
    }
}