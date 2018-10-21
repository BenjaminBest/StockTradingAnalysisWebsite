using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Core.Services;

namespace StockTradingAnalysis.Core.Tests.Services
{
	[TestClass]
	public class MathCalculatorServiceTests
	{
		[TestMethod]
		[Description("CalculateGeometricMean should not throw exception for null")]
		public void CalculateGeometricMeanShouldNotThrowExceptionForNull()
		{
			Action act = () => new MathCalculatorService().CalculateGeometricMean(null);
			act.Should().NotThrow();
		}

		[TestMethod]
		[Description("CalculateGeometricMean should not throw exception for an empty list")]
		public void CalculateGeometricMeanShouldNotThrowExceptionForEmptyList()
		{
			Action act = () => new MathCalculatorService().CalculateGeometricMean(Enumerable.Empty<decimal>());
			act.Should().NotThrow();
		}

		[TestMethod]
		[Description("CalculateGeometricMean should return 0 for a list of zeros")]
		public void CalculateGeometricMeanShouldReturnZeroForAListOfZeros()
		{
			var values = new List<decimal> { 0, 0 };
			var result = new MathCalculatorService().CalculateGeometricMean(values);
			result.Should().Be(0);
		}

		[TestMethod]
		[Description("CalculateGeometricMean should return the correct result")]
		public void CalculateGeometricMeanShouldReturnCorrectResult1()
		{
			var values = new List<decimal> { 1.05m, 1.03m, 0.94m, 1.02m, 1.04m };
			var result = new MathCalculatorService().CalculateGeometricMean(values);
			result.Should().Be(1.02m);
		}

		[TestMethod]
		[Description("CalculateGeometricMean should return the correct result")]
		public void CalculateGeometricMeanShouldReturnCorrectResult2()
		{
			var values = new List<decimal> { 0.5m, 0.5m, 0.5m };
			var result = new MathCalculatorService().CalculateGeometricMean(values);
			result.Should().Be(0.5m);
		}

		[Ignore]
		[TestMethod]
		[Description("CalculateGeometricMean should return the correct result")]
		public void CalculateGeometricMeanShouldReturnCorrectResult3()
		{
			var values = new List<decimal> { 0.1m, 0.2m, 0.3m, 0.4m, 0.5m };
			var result = new MathCalculatorService().CalculateGeometricMean(values);
			result.Should().Be(0.26m);
		}

		[Ignore]
		[TestMethod]
		[Description("CalculateGeometricMean should return the correct result")]
		public void CalculateGeometricMeanShouldReturnCorrectResult4()
		{
			var values = new List<decimal> { 12m, 4m, 2m };
			var result = new MathCalculatorService().CalculateGeometricMean(values);
			result.Should().Be(4.57m);
		}

		[TestMethod]
		[Description("CalculateGeometricMean should not throw overflow exception")]
		public void CalculateGeometricMeanShouldNotThrowOverflowException()
		{
			var values = new List<decimal> { -96.86m, -98.28m, -99.69m };

			Action act = () => new MathCalculatorService().CalculateGeometricMean(values);
			act.Should().NotThrow();
		}


		[TestMethod]
		[Description("CalculateGeometricMean should return the correct result")]
		public void CalculateGeometricMeanShouldReturnCorrectResult5()
		{
			var values = new List<decimal> { 23.5m, 33.6m, -18.8m, 14.4m, -14.9m, -1m, 3m, 96.2m, 3.4m, 0m };
			var result = new MathCalculatorService().CalculateGeometricMean(values);
			result.Should().Be(10.44m);
		}
	}
}
