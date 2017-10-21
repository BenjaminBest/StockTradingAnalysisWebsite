using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.EventSourcing.Messaging
{
    /// <summary>
    /// The interface IEventBus defines a bus, which starts event handler by incoming events and raises events.
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly IDependencyService _dependencyService;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="dependencyService"></param>
        public EventBus(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }

        /// <summary>
        /// Raises the given event <paramref name="domainEvent"/>
        /// </summary>
        /// <typeparam name="TEvent">Type of the event</typeparam>
        /// <param name="domainEvent"></param>
        public void Publish<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
            var handlerType = typeof (IEventHandler<>).MakeGenericType(domainEvent.GetType());

            foreach (var handler in _dependencyService.GetServices(handlerType))
            {
                var dyn = (dynamic) handler;

                dyn.Handle((dynamic) domainEvent);
            }
        }
    }
}