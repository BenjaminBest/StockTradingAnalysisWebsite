namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface IModelRepository{TItem} defines an repository with reader and writer access
    /// </summary>
    /// <typeparam name="TItem">The type of the domain item</typeparam>
    public interface IModelRepository<TItem> : IModelReaderRepository<TItem>, IModelWriterRepository<TItem>
        where TItem : class, IModelRepositoryItem
    {
    }
}