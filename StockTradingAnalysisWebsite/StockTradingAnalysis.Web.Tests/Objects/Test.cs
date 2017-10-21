using System;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class Test : IModelRepositoryItem
    {
        public Guid Id { get; set; }
        public string Wkn { get; set; }
        public string Name { get; set; }
        public bool IsDividend { get; set; }

        public int OriginalVersion { get; set; }

        public Test(Guid id)
        {
            Id = id;
        }
    }
}