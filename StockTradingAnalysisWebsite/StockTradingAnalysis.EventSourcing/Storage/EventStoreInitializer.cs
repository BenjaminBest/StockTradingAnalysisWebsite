using StockTradingAnalysis.EventSourcing.Exceptions;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// The EventStoreInitializer initializes the event store
    /// </summary>
    public class EventStoreInitializer : IEventStoreInitializer
    {
        private readonly IEventStore _eventStore;
        private readonly IEventBus _eventBus;

        private static bool _isInitialized;

        /// <summary>
        /// Initializes this object
        /// </summary>
        public EventStoreInitializer(IEventStore eventStore, IEventBus eventBus)
        {
            _eventStore = eventStore;
            _eventBus = eventBus;
        }

        /// <summary>
        /// Instructs the eventstore <seealso cref="IEventStore"/> to load all events, 
        /// processes them and publish them on the eventbus <seealso cref="IEventBus"/>
        /// </summary>
        public void Replay()
        {
            if (_isInitialized)
                throw new EventStoreInitializeException(
                    "The eventstore is already initialized. This can only be done once at startup");

            foreach (var @event in _eventStore.GetEvents())
            {
                _eventBus.Publish(@event);
            }

            _isInitialized = true;
        }
    }
}