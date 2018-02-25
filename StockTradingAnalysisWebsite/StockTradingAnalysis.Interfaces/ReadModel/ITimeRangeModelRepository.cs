using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface ITimeRangeStatisticsModelRepository defines a repository which stores time range based statistics.
    /// </summary>
    public interface ITimeRangeModelRepository<TItem> : ITimeRangeModelReaderRepository<TItem>, ITimeRangeModelWriterRepository<TItem> where TItem : ITimeRangeKey
    {
        /// <summary>
        /// Deletes all.
        /// </summary>
        void DeleteAll();
    }
}