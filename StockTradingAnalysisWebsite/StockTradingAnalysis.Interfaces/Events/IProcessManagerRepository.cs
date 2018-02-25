using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IProcessManagerRepository defines a repository for process managers. Once a process manager was instantiated it 
    /// will be added to this repository based on a correlation/business id wich identifies a business process. 
    /// </summary>
    public interface IProcessManagerRepository
    {
        /// <summary>
        /// Gets the process manager instance by the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An existent instance of a process manager.</returns>
        IProcessManager GetById(Guid id);

        /// <summary>
        /// Returns all items in this repository
        /// </summary>
        /// <returns>All items</returns>
        IEnumerable<IProcessManager> GetAll();

        /// <summary>
        /// Deletes the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be deleted</param>
        void Delete(IProcessManager item);


        /// <summary>
        /// Adds the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be added</param>
        void Add(IProcessManager item);

        /// <summary>
        /// Deletes all.
        /// </summary>
        void DeleteAll();
    }
}