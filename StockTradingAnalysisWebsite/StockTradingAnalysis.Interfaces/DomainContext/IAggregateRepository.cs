using System;

namespace StockTradingAnalysis.Interfaces.DomainContext
{
    /// <summary>
    /// IAggregateRepository{TAggregate} defines an interface for an repository which wraps one aggregate
    /// </summary>
    /// <typeparam name="TAggregate"></typeparam>
    public interface IAggregateRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        /// <summary>
        /// Returns the aggregate with the given <paramref name="id"/> or creates a new one
        /// </summary>
        /// <param name="id">The id of the aggregate</param>
        /// <returns>Current state of the aggregate</returns>
        TAggregate GetById(Guid id);

        /// <summary>
        /// Saves all uncommitted changes of the aggregate to the event store
        /// </summary>
        /// <param name="aggregate">The aggregate</param>
        /// <param name="expectedVersion">The current version of the latest event for an aggregate</param>
        void Save(TAggregate aggregate, int expectedVersion);
    }
}