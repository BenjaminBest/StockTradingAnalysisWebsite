using System;
using StockTradingAnalysis.EventSourcing.Exceptions;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.DomainContext
{
    /// <summary>
    /// AggregateRepository wraps all operations on an aggregate
    /// </summary>
    /// <typeparam name="TAggregate">The type of the aggregate</typeparam>
    public class AggregateRepository<TAggregate> : IAggregateRepository<TAggregate>, ISnapshotableRepository
        where TAggregate : IAggregateRoot, new()
    {
        private readonly IEventStore _eventStore;
        private readonly ISnapshotStore _snapshotStore;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="eventStore">The event store</param>
        /// <param name="snapshotStore">The snapshot store</param>
        public AggregateRepository(IEventStore eventStore, ISnapshotStore snapshotStore)
        {
            _eventStore = eventStore;
            _snapshotStore = snapshotStore;
        }

        /// <summary>
        /// Returns the aggregate with the given <paramref name="id"/> or creates a new one.
        /// </summary>
        /// <param name="id">The id of the aggregate</param>
        /// <returns>Current state of the aggregate</returns>
        public TAggregate GetById(Guid id)
        {
            var snapshot = IsSnapshotSupported() ? _snapshotStore.GetSnapshot(id) : null;

            var events = snapshot != null
                ? _eventStore.GetEventsByAggregateId(id, snapshot.Version)
                : _eventStore.GetEventsByAggregateId(id);

            var aggregate = new TAggregate();

            if (snapshot != null)
                ((ISnapshotOriginator)aggregate).SetSnapshot(snapshot);

            aggregate.LoadFromHistory(events);

            return aggregate;
        }

        /// <summary>
        /// Saves all uncommitted changes of the aggregate to the event store
        /// </summary>
        /// <param name="item">The aggregate</param>
        /// <param name="expectedVersion">The current version of the latest event for an aggregate</param>
        public void Save(TAggregate item, int expectedVersion)
        {
            if (!item.HasPendingChanges())
                return;

            _eventStore.Save(item.GetUncommittedChanges(), expectedVersion);
            item.MarkChangesAsCommited();
        }

        /// <summary>
        /// Returns the originator aka an aggregate which supports snapshots
        /// </summary>
        /// <param name="aggregateId">The Id of an aggregate</param>
        /// <returns>The snaphot originator</returns>
        /// <exception cref="SnapshotNotSupportedException">Thrown if <typeparamref name="TAggregate"/>doen not support snapshots</exception>
        public ISnapshotOriginator GetOriginator(Guid aggregateId)
        {
            if (!IsSnapshotSupported())
                throw new SnapshotNotSupportedException();

            return GetById(aggregateId) as ISnapshotOriginator;
        }

        /// <summary>
        /// Returns if underlying aggregate supports snapshots
        /// </summary>
        /// <returns><c>True</c>if snapshot is supported</returns>
        public bool IsSnapshotSupported()
        {
            return typeof(ISnapshotOriginator).IsAssignableFrom(GetOriginatorType());
        }

        /// <summary>
        /// Returns the type of the originator
        /// </summary>
        /// <returns>Type of the originator</returns>
        public Type GetOriginatorType()
        {
            return typeof(TAggregate);
        }
    }
}