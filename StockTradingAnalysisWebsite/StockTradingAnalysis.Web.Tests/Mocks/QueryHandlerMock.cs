using Moq;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Web.Tests.Mocks
{
    public class QueryHandlerMock
    {
        public static IQueryHandler<IQuery<TObject>, TObject> GetMock<TObject>(TObject returnValue) where TObject : class
        {
            var mock = new Mock<IQueryHandler<IQuery<TObject>, TObject>>();
            mock.Setup(s => s.Execute(It.IsAny<IQuery<TObject>>())).Returns(returnValue);

            return mock.Object;
        }
    }
}