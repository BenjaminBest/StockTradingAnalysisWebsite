using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Process.ProcessManagers
{
    /// <summary>
    /// The class ProcessManagerBase is a base class for all process managers.
    /// </summary>
    /// <seealso cref="IProcessManager" />
    public class ProcessManagerBase<TData> : IProcessManager where TData : IProcessManagerData, new()
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
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public TData Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessManagerBase{TData}" /> class.
        /// </summary>
        public ProcessManagerBase()
        {
            Data = new TData();
        }

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
