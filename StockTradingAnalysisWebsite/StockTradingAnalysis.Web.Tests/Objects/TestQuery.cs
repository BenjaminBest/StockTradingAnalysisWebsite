using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestQuery : IQuery<Test>
    {
        public string Id { get; set; }

        public TestQuery()
        {

        }

        public TestQuery(string id)
        {
            Id = id;
        }
    }
}