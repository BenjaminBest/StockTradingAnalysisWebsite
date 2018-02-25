using System;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IMessageCorrelationIdCreator defines methods to identify a process manager based on some correlation id. Information in an event or a command is used
    /// to identify the manager. The IMessageCorrelationIdCreator configured a mapping for one process manager.
    /// </summary>
    public interface IMessageCorrelationIdCreator<in TMessage>
        where TMessage : IMessage
    {
        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Guid GetCorrelationId(TMessage message);
    }
}