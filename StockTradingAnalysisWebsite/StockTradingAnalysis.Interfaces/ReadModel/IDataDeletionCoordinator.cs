namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface IModelRepositoryDeletionCoordinator defines methods to delete all data in the system.
    /// </summary>
    public interface IModelRepositoryDeletionCoordinator
    {
        /// <summary>
        /// Deletes all.
        /// </summary>
        void DeleteAll();
    }
}