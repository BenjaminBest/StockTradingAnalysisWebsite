using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using StockTradingAnalysis.Domain.Process.Exceptions;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Domain.Process.Repository
{
    /// <summary>
    /// The class ProcessManagerRepository is a repository to store instances of process managers
    /// </summary>
    /// <seealso cref="IProcessManagerRepository" />
    public class ProcessManagerRepository : IProcessManagerRepository, ISupportsDataDeletion
    {
        /// <summary>
        /// The managers
        /// </summary>
        private readonly IDictionary<Guid, IProcessManager> _managers = new ConcurrentDictionary<Guid, IProcessManager>();

        /// <summary>
        /// Returns the item with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>The item with the <paramref name="id"/> or <c>null</c></returns>
        public IProcessManager GetById(Guid id)
        {
            IProcessManager model;

            return _managers.TryGetValue(id, out model) ? model : null;
        }

        /// <summary>
        /// Returns all items in this repository
        /// </summary>
        /// <returns>All items</returns>
        public IEnumerable<IProcessManager> GetAll()
        {
            return _managers.Values;
        }

        /// <summary>
        /// Deletes the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be deleted</param>
        /// <exception cref="ProcessManagerRepositoryDeleteException">Thrown if <paramref name="item"/> is null or has an empty id</exception>
        public void Delete(IProcessManager item)
        {
            if (item == null)
                throw new ProcessManagerRepositoryDeleteException("The given item was null");

            if (item.Id == Guid.Empty)
                throw new ProcessManagerRepositoryDeleteException("The given item has an empty id");

            if (_managers.ContainsKey(item.Id))
            {
                _managers.Remove(item.Id);
            }
            else
            {
                throw new ProcessManagerRepositoryDeleteException($"The given item cannot be deleted because the id '{item.Id}' does not exist");
            }
        }

        /// <summary>
        /// Adds the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be added</param>
        /// <exception cref="ProcessManagerRepositoryAddException">Thrown if <paramref name="item"/> is null, has an empty id or already exists</exception>
        public void Add(IProcessManager item)
        {
            if (item == null)
                throw new ProcessManagerRepositoryAddException("The given item was null");

            if (item.Id == Guid.Empty)
                throw new ProcessManagerRepositoryAddException("The given item has an empty id");

            if (_managers.ContainsKey(item.Id))
                throw new ProcessManagerRepositoryAddException($"The given item with the id '{item.Id}' already exists");

            _managers.Add(item.Id, item);
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        public void DeleteAll()
        {
            _managers.Clear();
        }
    }
}