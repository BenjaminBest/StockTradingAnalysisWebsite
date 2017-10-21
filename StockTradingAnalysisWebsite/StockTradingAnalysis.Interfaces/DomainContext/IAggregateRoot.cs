using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.DomainContext
{
    /// <summary>
    /// IAggregateRoot defines an interface for an aggregate root, which is a bounded context inside the domain.
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// Gets the aggregate id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Returns <c>true</c> if pending changes exist
        /// </summary>
        /// <returns><c>True</c> for pending changes</returns>
        bool HasPendingChanges();

        /// <summary>
        /// Returns all uncommited changes for the aggregate
        /// </summary>
        /// <returns>A list of uncommited changes</returns>
        IEnumerable<IDomainEvent> GetUncommittedChanges();

        /// <summary>
        /// Marks all changes as commited
        /// </summary>
        void MarkChangesAsCommited();

        /// <summary>
        /// Apply all historical events/changes to this aggregate to build current status
        /// </summary>
        /// <param name="eventHistory">List of all historical events</param>
        void LoadFromHistory(IEnumerable<IDomainEvent> eventHistory);
    }
}