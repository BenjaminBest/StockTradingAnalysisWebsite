using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.ReadModel
{
    /// <summary>
    /// The interface ITimeSliceModelRepository defines a repository which stores time range based statistics.
    /// </summary>
    public interface ITimeSliceModelRepository<TItem> : ITimeSliceModelReaderRepository<TItem>, ITimeSliceModelWriterRepository<TItem> where TItem : ITimeSliceKey
    {
        /// <summary>
        /// Deletes all.
        /// </summary>
        void DeleteAll();
    }
}