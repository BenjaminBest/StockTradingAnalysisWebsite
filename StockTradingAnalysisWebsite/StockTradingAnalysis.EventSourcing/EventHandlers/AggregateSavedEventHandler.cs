using StockTradingAnalysis.EventSourcing.Events;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.EventHandlers
{
    /// <summary>
    /// The AggregateSavedEventHandler starts the snapshot processor <seealso cref="ISnapshotProcessor"/> to create snapshots.
    /// </summary>
    public class AggregateSavedEventHandler : IEventHandler<AggregateSavedEvent>
    {
        private readonly ISnapshotProcessor _snapshotProcessor;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="snapshotProcessor">The snapshot processor</param>
        public AggregateSavedEventHandler(ISnapshotProcessor snapshotProcessor)
        {
            _snapshotProcessor = snapshotProcessor;
        }

        /// <summary>
        /// Starts the snapshot processor asynchronous
        /// </summary>
        /// <param name="eventData">The event data</param>
        public void Handle(AggregateSavedEvent eventData)
        {
            if (_snapshotProcessor.IsSnapshotNeeded(eventData.AggregateId, eventData.AggregateType, eventData.Version))
                _snapshotProcessor.CreateSnapshot(eventData.AggregateId, eventData.AggregateType);
        }
    }
}