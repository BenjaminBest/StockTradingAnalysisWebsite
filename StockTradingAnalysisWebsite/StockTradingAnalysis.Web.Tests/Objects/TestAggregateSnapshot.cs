using StockTradingAnalysis.Interfaces.DomainContext;
using System;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestAggregateSnapshot : SnapshotBase
    {
        public string WKN { get; private set; }
        public string Name { get; private set; }
        public bool IsDividend { get; private set; }

        public TestAggregateSnapshot(Guid id, int version, string wkn, string name, bool isDividend)
        {
            AggregateId = id;
            Version = version;
            WKN = wkn;
            Name = name;
            IsDividend = isDividend;
        }
    }
}
