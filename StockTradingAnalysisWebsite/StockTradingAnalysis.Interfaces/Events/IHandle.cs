namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IHandle{TEvent} defines a marker for an aggregate which events can be handled
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IHandle<in TEvent> where TEvent : IDomainEvent
    {
        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        void Handle(TEvent @event);
    }
}