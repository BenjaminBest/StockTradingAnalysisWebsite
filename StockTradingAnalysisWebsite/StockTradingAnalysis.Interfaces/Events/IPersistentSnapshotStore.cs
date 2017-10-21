using StockTradingAnalysis.Interfaces.DomainContext;

namespace StockTradingAnalysis.Interfaces.Events
{
    /// <summary>
    ///     The interface defines an in memory storage for mementos/snapshots
    /// </summary>
    public interface IPersistentSnapshotStore : IPersistentStore<SnapshotBase>
    {
    }
}