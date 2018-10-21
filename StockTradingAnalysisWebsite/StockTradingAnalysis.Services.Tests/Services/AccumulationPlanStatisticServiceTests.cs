using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Services;

namespace StockTradingAnalysis.Services.Tests.Services
{
	[TestClass]
	public class AccumulationPlanStatisticServiceTests
	{
		private static AccumulationPlanStatisticService Initialize(List<ITransaction> transactions = null)
		{
			var dateCalcService = new Mock<IDateCalculationService>();
			var dispatcher = new Mock<IQueryDispatcher>();
			var transactionService = new Mock<ITransactionCalculationService>();
			var iirCalculator = new Mock<IInterestRateCalculatorService>();

			dispatcher.Setup(s => s.Execute(It.IsAny<StockQuotationsByIdQuery>())).Returns(new List<IQuotation>());
			dispatcher.Setup(s => s.Execute(It.IsAny<TransactionAllQuery>())).Returns(transactions ?? new List<ITransaction>());

			return new AccumulationPlanStatisticService(dateCalcService.Object, dispatcher.Object, iirCalculator.Object, transactionService.Object);
		}

		//TODO: Calculate
		//TODO: CalculateSumInpayment
		//TODO: CalculateSumCapital
		//TODO: CalculatePerformancePercentageIIR

		[TestMethod]
		[Description("Calculate should return null if no transaction are available")]
		public void CalculateShouldNotThrowIfNoTransactionAreAvailable()
		{

			Action act = () => Initialize().CalculateCurrentPeriod(DateTime.MinValue, DateTime.MaxValue, "tag");
			act.Should().NotThrow();

			Action actOverall = () => Initialize().CalculateOverallPeriod(DateTime.MinValue, DateTime.MaxValue, "tag");
			actOverall.Should().NotThrow();
		}
	}
}
