using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Domain.Events.Aggregates
{
    /// <summary>
    /// AggregateRoot defines the root for all aggregates, which is a bounded context inside the domain.
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly HashSet<IDomainEvent> _changes = new HashSet<IDomainEvent>();

        protected AggregateRoot()
        {
            Version = -1;
        }

        /// <summary>
        /// Gets the aggregate id
        /// </summary>
        public abstract Guid Id { get; protected set; }

        /// <summary>
        /// Gets the aggregate version (equals last applied version)
        /// </summary>
        protected int Version { get; set; }

        /// <summary>
        /// Returns <c>true</c> if pending changes exist
        /// </summary>
        /// <returns><c>True</c> for pending changes</returns>
        public bool HasPendingChanges()
        {
            return _changes.Any();
        }

        /// <summary>
        /// Returns all uncommited changes for the aggregate
        /// </summary>
        /// <returns>A list of uncommited changes</returns>
        public IEnumerable<IDomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        /// <summary>
        /// Marks all changes as commited
        /// </summary>
        public void MarkChangesAsCommited()
        {
            _changes.Clear();
        }

        /// <summary>
        /// Apply all historical events/changes to this aggregate to build current status
        /// </summary>
        /// <param name="eventHistory">List of all historical events</param>
        public void LoadFromHistory(IEnumerable<IDomainEvent> eventHistory)
        {
            foreach (var @event in eventHistory)
            {
                ApplyChange(@event, false);
            }
        }

        /// <summary>
        /// Calls the actual instance of this base class to handle the current event and applies all changes
        /// to the instance.
        /// </summary>
        /// <param name="event">Domain event</param>
        protected void ApplyChange(IDomainEvent @event)
        {
            ApplyChange(@event, true);
        }

        /// <summary>
        /// Calls the actual instance of this base class to handle the current event and applies all changes
        /// to the instance.
        /// </summary>
        /// <param name="event">Domain event</param>
        /// <param name="isNew"><c>True</c>if this is no historical event</param>
        private void ApplyChange(IDomainEvent @event, bool isNew)
        {
            dynamic dyn = this;
            AggregateEventApplier.InvokeEvent(dyn, @event);

            //Events only have a version when they were loaded from the event store
            if (!isNew)
            {
                Version = @event.Version;
            }
            else
            {
                @event.Version = Version++;
            }

            //During replay no changes are open
            if (isNew)
                _changes.Add(@event);
        }
    }
}