using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface ITimeRangeModelWriterRepository defines a repository which stores time range based statistics.
    /// </summary>
    public interface ITimeRangeModelWriterRepository<in TItem> where TItem : ITimeRangeKey
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