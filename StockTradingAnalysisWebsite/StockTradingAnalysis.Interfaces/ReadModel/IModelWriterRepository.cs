namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface IModelWriterRepository{TItem} defines an repository for write access
    /// </summary>
    /// <typeparam name="TItem">The underlying domain model</typeparam>
    public interface IModelWriterRepository<in TItem> where TItem : class, IModelRepositoryItem
    {
        /// <summary>
        /// Updates the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be updated</param>
        void Update(TItem item);

        /// <summary>
        /// Deletes the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be deleted</param>
        void Delete(TItem item);

        /// <summary>
        /// Adds the given <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to be added</param>
        void Add(TItem item);
    }
}