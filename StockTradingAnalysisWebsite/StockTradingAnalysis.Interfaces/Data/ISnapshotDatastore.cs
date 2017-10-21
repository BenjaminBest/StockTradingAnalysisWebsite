using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Interfaces.Data
{
    /// <summary>
    /// The interface ISnapshotDatastore defines the interface for an document based database store specialized for snapshots
    /// </summary>
    public interface ISnapshotDatastore : IDatastore<SnapshotBase>
    {
    }
}