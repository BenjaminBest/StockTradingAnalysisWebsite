using Raven.Client.Indexes;
using StockTradingAnalysis.Interfaces.DomainContext;
using System.Linq;

namespace StockTradingAnalysis.Data.RavenDb.Indexes
{
    public class SnapshotIndex : AbstractIndexCreationTask<SnapshotBase>
    {
        public SnapshotIndex()
        {
            Map = snapshots => from s in snapshots
                               select new
                               {
                                   s.AggregateId
                               };
        }
    }
}