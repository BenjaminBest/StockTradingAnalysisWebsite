using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.Events
{
    /// <summary>
    /// The ReplayFinishedEvent is fired when the <see cref="IEventStoreInitializer"/> has finished the event replay.
    /// </summary>
    /// <seealso cref="DomainEvent" />
    public class ReplayFinishedEvent : DomainEvent
    {
    }
}