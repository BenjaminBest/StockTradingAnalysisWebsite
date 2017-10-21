namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// Interface IEventHandler defines an handler for a specific event
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        /// <summary>
        /// Processes the given event <paramref name="eventData"/>
        /// </summary>
        /// <param name="eventData">Event data</param>
        void Handle(TEvent eventData);
    }
}