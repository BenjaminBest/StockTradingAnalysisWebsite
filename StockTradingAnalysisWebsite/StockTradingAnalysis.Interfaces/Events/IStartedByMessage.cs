namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IStartedByMessage{TMessage} is implemented by a concrete process handler and describes the messages which trigger the creation of a
    /// process handler which then starts the coordination of this process.
    /// 
    /// Basically it's the same interface like <seealso cref="IHandle{TEvent}"/>, but this one is used by a process handler whereas IHandle is
    /// implemented by an event handler.
    /// </summary>
    public interface IStartedByMessage<in TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Handles the given message <paramref name="message" />
        /// </summary>
        /// <param name="message">The message.</param>
        void Handle(TMessage message);
    }
}