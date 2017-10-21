using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.EventSourcing.Events;
using StockTradingAnalysis.EventSourcing.Exceptions;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// The InMemoryEventStore writes and reads events from the internal storage and publishes all received events on the
    /// <seealso cref="IEventBus" />.
    /// </summary>
    public class EventStore : IEventStore
    {
        private readonly IEventBus _eventBus;
        private readonly IPersistentEventStore _persistentEventStore;
        private readonly IPerformanceMeasurementService _performanceMeasurementService;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="eventBus">The eventbus</param>
        /// <param name="persistentEventStore">The persistence layer for the event store</param>
        /// <param name="performanceMeasurementService">The performance measurement service</param>
        public EventStore(
            IEventBus eventBus,
            IPersistentEventStore persistentEventStore,
            IPerformanceMeasurementService performanceMeasurementService)
        {
            _eventBus = eventBus;
            _persistentEventStore = persistentEventStore;
            _performanceMeasurementService = performanceMeasurementService;
        }

        /// <summary>
        /// Returns all events order by the timestamp.
        /// </summary>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> GetEvents()
        {
            return _persistentEventStore.All();
        }

        /// <summary>
        /// Returns all events which aggregateId matches the given <paramref name="aggregateId" />.
        /// </summary>
        /// <param name="aggregateId">The identifier by an aggregate</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> GetEventsByAggregateId(Guid aggregateId)
        {
            return _persistentEventStore.Find(aggregateId);
        }

        /// <summary>
        /// Returns all events which aggregateId matches the given <paramref name="aggregateId" /> and the minimum version
        /// <paramref name="minVersion" />
        /// </summary>
        /// <param name="aggregateId">The identifier of an aggregate</param>
        /// <param name="minVersion">A minimum version for the aggregate</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> GetEventsByAggregateId(Guid aggregateId, int minVersion)
        {
            return _persistentEventStore.Find(aggregateId, minVersion);
        }

        /// <summary>
        /// Saves the given <paramref name="events" /> to the store.
        /// </summary>
        /// <param name="events">A list of events</param>
        /// <param name="expectedVersion">The current version of the latest event for an aggregate</param>
        public void Save(IEnumerable<IDomainEvent> events, int expectedVersion)
        {
            var eventList = events.ToList();
            var firstEvent = eventList.FirstOrDefault();

            if (firstEvent == null)
                throw new EventStoreSaveException();

            var aggregateId = firstEvent.AggregateId;
            var aggregateType = firstEvent.AggregateType;
            var newVersion = expectedVersion;

            using (new TimeMeasure(ms => _performanceMeasurementService.CountCommit(eventList.Count, ms)))
            {
                if (expectedVersion != -1)
                {
                    var lastVersion = GetEventsByAggregateId(aggregateId).Max(e => e.Version); //TODO: Performance-Optimization: Why loading all events and then only using the last

                    if (lastVersion != expectedVersion)
                        throw new ConcurrencyException();
                }

                foreach (var @event in eventList)
                {
                    newVersion++;
                    @event.Version = newVersion;

                    //_persistentEventStore.Add(@event);

                    //Create snapshot
                    //_eventBus.Publish(new AggregateSavedEvent(aggregateId, aggregateType, newVersion));
                }

                _persistentEventStore.Add(eventList);

                //Create snapshot
                _eventBus.Publish(new AggregateSavedEvent(aggregateId, aggregateType, newVersion));
            }

            //Publish events
            eventList.ForEach(e => _eventBus.Publish(e));
        }
    }
}