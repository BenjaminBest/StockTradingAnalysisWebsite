namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    /// The interface IProcessManagerCoordinator defines a locator which tries to find an instance of a concrete process manager which
    /// is capable of handling specific events and commands. If such an instance already exists in the repository identified by
    /// a correlation id it will be returned.
    /// If not, a new instance will be created and added to the repository.
    /// </summary>
    public interface IProcessManagerCoordinator
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();
    }
}