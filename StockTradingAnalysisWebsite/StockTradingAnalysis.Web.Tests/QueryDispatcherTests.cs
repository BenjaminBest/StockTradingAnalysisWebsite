using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.CQRS.Exceptions;
using StockTradingAnalysis.CQRS.QueryDispatcher;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
	[TestClass]
	public class QueryDispatcherTests
	{
		[TestMethod]
		[Description("Query dispatcher should return object from the query handler")]
		public void QueryDispatcherShouldReturnObjectOfQueryHandler()
		{
			var resultObj = new Test(Guid.NewGuid());

			DependencyServiceMock.SetMock(new DependencyDescriptor(typeof(IQueryHandler<TestQuery, Test>),
				QueryHandlerMock.GetMock(resultObj)));

			var dispatcher = new QueryDispatcher(PerformanceCounterMock.GetMock());
			var result = dispatcher.Execute(new TestQuery());

			result.Should().Equals(resultObj);
		}

		[TestMethod]
		[Description("Query dispatcher should throw QueryDispatcherException when handler not found")]
		public void QueryDispatcherShouldThrowExceptionIfHandlerNotFound()
		{
			DependencyServiceMock.SetMock(new DependencyDescriptor(typeof(IQueryHandler<TestQuery, Test>), null));

			var dispatcher = new QueryDispatcher(PerformanceCounterMock.GetMock());

			Action act = () => dispatcher.Execute(new TestQuery());
			act.Should().Throw<QueryDispatcherException>();
		}
	}
}