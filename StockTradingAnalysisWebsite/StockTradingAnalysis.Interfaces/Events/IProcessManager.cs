using System;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The process manger
    /// </summary>
    public interface IProcessManager
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Registers for status update.
        /// </summary>
        /// <param name="callback">The callback.</param>
        void RegisterForStatusUpdate(IProcessManagerStatusCallback callback);
    }
}