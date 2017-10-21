namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IEventStoreInitializer defines an initializer component for the event store
    /// </summary>
    public interface IEventStoreInitializer
    {
        /// <summary>
        /// Instructs the eventstore <seealso cref="IEventStore"/> to load all events, 
        /// processes them and publish them on the eventbus <seealso cref="IEventBus"/>
        /// </summary>
        void Replay();
    }
}