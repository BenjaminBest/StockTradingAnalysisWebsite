using System;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IMessage defines a message which can be a command or an event.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the id of this event
        /// </summary>
        Guid Id { get; }
    }
}
