namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface IModelRepositorySupportsDataDeletion defines methods to delete data, in memory or persisted.
    /// </summary>
    public interface IModelRepositorySupportsDataDeletion
    {
        /// <summary>
        /// Deletes all.
        /// </summary>
        void DeleteAll();
    }
}