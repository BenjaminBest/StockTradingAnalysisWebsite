namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface ISupportsDataDeletion defines methods to delete data, in memory or persisted.
    /// </summary>
    public interface ISupportsDataDeletion
    {
        /// <summary>
        /// Deletes all.
        /// </summary>
        void DeleteAll();
    }
}