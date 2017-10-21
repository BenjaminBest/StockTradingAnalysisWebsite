using StockTradingAnalysis.Domain.CQRS.Query.ReadModel;

namespace StockTradingAnalysis.Web.Tests.Objects
{
    public class TestModelRepository : ModelRepositoryBase<Test>
    {
        public void ClearAll()
        {
            _items.Clear();
        }
    }
}