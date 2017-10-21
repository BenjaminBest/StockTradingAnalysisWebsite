using StockTradingAnalysis.EventSourcing.Events;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    /// <summary>
    /// The AggregateSavedEventHandler starts the snapshot processor <seealso cref="ISnapshotProcessor"/> to create snapshots.
    /// </summary>
    public class TestAggregateSavedEventHandler : IEventHandler<AggregateSavedEvent>
    {
        public ISnapshotProcessor Processor { get; set; }

        /// <summary>
        /// Starts the snapshot processor synchronous
        /// </summary>
        /// <param name="eventData">The event data</param>
        public void Handle(AggregateSavedEvent eventData)
        {
            if (Processor.IsSnapshotNeeded(eventData.AggregateId, eventData.AggregateType, eventData.Version))
                Processor.CreateSnapshot(eventData.AggregateId, eventData.AggregateType);
        }
    }
}