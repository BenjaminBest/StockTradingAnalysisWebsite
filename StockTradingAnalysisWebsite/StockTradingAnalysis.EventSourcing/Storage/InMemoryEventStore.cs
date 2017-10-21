using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    public class InMemoryEventStore : IPersistentEventStore
    {
        private readonly IList<IDomainEvent> _events = new List<IDomainEvent>();

        /// <summary>
        /// Adds the given <paramref name="@event" /> to the internal storage
        /// </summary>
        /// <param name="event"></param>
        public void Add(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        /// <summary>
        /// Adds the given <paramref name="@events" /> to the internal storage
        /// </summary>
        /// <param name="events"></param>
        public void Add(IEnumerable<IDomainEvent> events)
        {
            foreach (var item in events)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Returns all items from the internal storage
        /// </summary>
        /// <returns>A list with all items</returns>
        public IEnumerable<IDomainEvent> All()
        {
            return _events.OrderBy(e => e.TimeStamp);
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> Find(Guid aggregateId)
        {
            return _events.Where(e => e.AggregateId.Equals(aggregateId)).OrderBy(e => e.TimeStamp);
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> and the <paramref name="minVersion"/> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <param name="minVersion">The minimum version of the aggregate</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> Find(Guid aggregateId, int minVersion)
        {
            return _events.Where(e => e.AggregateId.Equals(aggregateId) && e.Version > minVersion).OrderBy(e => e.TimeStamp);
        }
    }
}