namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IMessageObserver defines an observer which will be notified in case of any new message.
    /// </summary>
    public interface IMessageObserver
    {
        /// <summary>
        /// Notifies the observer about the <paramref name="command" />.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="command">The command.</param>
        void Notify<TMessage>(TMessage command) where TMessage : IMessage;
    }
}