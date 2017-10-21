using StockTradingAnalysis.Interfaces.Events;
using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Data
{
    /// <summary>
    /// The interface IEventDatastore defines the interface for an document based database store specialized for events
    /// </summary>
    public interface IEventDatastore : IDatastore<IDomainEvent>
    {
        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <param name="minVersion">The minimum version of the aggregate</param>
        /// <returns>A list with all events</returns>
        IEnumerable<IDomainEvent> Select(Guid aggregateId, int minVersion);
    }
}