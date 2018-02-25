using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Process.ProcessManagers
{
    /// <summary>
    /// The class ProcessManagerBase is a base class for all process managers.
    /// </summary>
    /// <seealso cref="IProcessManager" />
    public class ProcessManagerBase : IProcessManager
    {
        /// <summary>
        /// Gets the status update.
        /// </summary>
        /// <value>
        /// The status update.
        /// </value>
        protected IProcessManagerStatusCallback StatusUpdate { get; private set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Registers for status update.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void RegisterForStatusUpdate(IProcessManagerStatusCallback callback)
        {
            StatusUpdate = callback;
        }
    }
}
