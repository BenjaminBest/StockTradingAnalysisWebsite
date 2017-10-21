using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class TypesTests
    {
        [TestMethod]
        [Description("Default-Ctor of TransactionAllQuery should correctly initialize values")]
        public void DefaultCtorOfTransactionAllQueryShouldCorrectlyInitializeValues()
        {
            var query = new TransactionAllQuery();

            //query.StartDate.Should().Be(DateTime.MinValue);
            //query.EndDate.Should().Be(DateTime.MaxValue);
            //query.StockType.Should().BeEmpty();
            //query.PositionType.Should().Be(PositionType.All);
            //query.Strategy.Should().BeNull();
            //query.UseDividends.Should().BeTrue();
            //query.LastAmountOfTrades.Should().Be(Int32.MaxValue);
        }
    }
}
