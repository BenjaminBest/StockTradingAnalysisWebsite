namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IEventBus defines a bus, which starts event handler by incoming events and raises events.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Raises the given event <paramref name="domainEvent"/>
        /// </summary>
        /// <typeparam name="TEvent">Type of the event</typeparam>
        /// <param name="domainEvent"></param>
        void Publish<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}