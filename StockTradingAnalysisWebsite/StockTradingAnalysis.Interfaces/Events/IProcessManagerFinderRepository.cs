namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IProcessManagerFinderRepository defines a repository which stores all process manager finders.
    /// </summary>
    public interface IProcessManagerFinderRepository : IProcessManagerStatusCallback
    {
        /// <summary>
        /// Gets the process manager based on the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        IProcessManager GetOrCreateProcessManager(IMessage message);

        /// <summary>
        /// Adds the process manager.
        /// </summary>
        /// <param name="processManager">The process manager.</param>
        void AddProcessManager(IProcessManager processManager);
    }
}