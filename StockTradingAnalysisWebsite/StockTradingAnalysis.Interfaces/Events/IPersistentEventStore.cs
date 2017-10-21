using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Events
{
    public interface IPersistentEventStore : IPersistentStore<IDomainEvent>
    {
        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <param name="minVersion">The minimum version of the aggregate</param>
        /// <returns>A list with all events</returns>
        IEnumerable<IDomainEvent> Find(Guid aggregateId, int minVersion);
    }
}