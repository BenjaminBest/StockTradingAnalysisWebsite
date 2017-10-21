using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestCommand : Command
    {
        public string Id { get; set; }

        public TestCommand() : base(-1, Guid.NewGuid())
        {
        }
    }
}