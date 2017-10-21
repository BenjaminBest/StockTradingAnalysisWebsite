namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface IModelRepositoryItem defined an item which can be used for the <seealso cref="IModelReaderRepository{TItem}"/>,
    /// <seealso cref="IModelWriterRepository{TItem}"/> or <seealso cref="IModelRepository{TItem}"/>
    /// </summary>
    public interface IVersionableModelRepositoryItem : IModelRepositoryItem
    {
        /// <summary>
        /// Gets/sets the version
        /// </summary>
        int OriginalVersion { get; set; }
    }
}