namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The IProcessManagerStatusCallback defines a callback mechanism for status changes in the concrete process manager instances.
    /// </summary>
    public interface IProcessManagerStatusCallback
    {
        /// <summary>
        /// Marks the process as completed.
        /// </summary>
        /// <param name="processManager">The process manager.</param>
        void MarkAsCompleted(IProcessManager processManager);
    }
}