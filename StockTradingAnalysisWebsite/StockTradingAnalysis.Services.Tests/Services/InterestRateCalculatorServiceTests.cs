using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Interfaces.Enumerations;
using StockTradingAnalysis.Interfaces.Types;
using StockTradingAnalysis.Services.Services;

namespace StockTradingAnalysis.Services.Tests.Services
{
    [TestClass]
    public class InterestRateCalculatorServiceTests
    {
        private readonly List<CashFlow> _cfs = new List<CashFlow>
        {
                new CashFlow(-10000, new DateTime(2008,1,1)),
                new CashFlow(2750, new DateTime(2008,3,1)),
                new CashFlow(4250, new DateTime(2008,10,30)),
                new CashFlow(3250, new DateTime(2009,2,15)),
                new CashFlow(2750, new DateTime(2009,4,1))
            };

        private readonly List<CashFlow> _bigcfs = new List<CashFlow> {
                new CashFlow(-10, new DateTime(2000,1,1)),
                new CashFlow(10, new DateTime(2002,1,2)),
                new CashFlow(20, new DateTime(2003,1,3))
            };

        private readonly List<CashFlow> _negcfs = new List<CashFlow> {
                new CashFlow(-10, new DateTime(2000,1,1)),
                new CashFlow(-1, new DateTime(2002,1,2)),
                new CashFlow(1, new DateTime(2003,1,3))
            };

        private readonly List<CashFlow> _samedaysamecfs = new List<CashFlow> {
                new CashFlow(-10, new DateTime(2000,1,1)),
                new CashFlow(10, new DateTime(2000,1,1)),
            };

        private readonly List<CashFlow> _samedaydifferentcfs = new List<CashFlow> {
                new CashFlow(-10, new DateTime(2000,1,1)),
                new CashFlow(100, new DateTime(2000,1,1)),
            };

        private readonly List<CashFlow> _bigratecfs = new List<CashFlow> {
                new CashFlow(-10, new DateTime(2000,1,1)),
                new CashFlow(20, new DateTime(2000,5,30)),
            };

        private readonly List<CashFlow> _zeroRate = new List<CashFlow> {
                new CashFlow(-10, new DateTime(2000,1,1)),
                new CashFlow(10, new DateTime(2003,1,1)),
                };

        private readonly List<CashFlow> _doubleNegative = new List<CashFlow> {
                new CashFlow(-10000, new DateTime(2008,1,1)),
                new CashFlow(2750, new DateTime(2008,3,1)),
                new CashFlow(-4250, new DateTime(2008,10,30)),
                new CashFlow(3250, new DateTime(2009,2,15)),
                new CashFlow(2750, new DateTime(2009,4,1))
            };

        private readonly List<CashFlow> _badDoubleNegative = new List<CashFlow> {
                new CashFlow(-10000, new DateTime(2008,1,1)),
                new CashFlow(2750, new DateTime(2008,3,1)),
                new CashFlow(-4250, new DateTime(2008,10,30)),
                new CashFlow(3250, new DateTime(2009,2,15)),
                new CashFlow(-2750, new DateTime(2009,4,1))
            };

        private const double R = 0.09;

        private const double Tolerance = 0.0001;

        private const int MaxIters = 100;

        [TestMethod]
        [Description("CalculateXNPV should calculate XNPV correct with small and big cashflows")]
        public void CalculateXNPVShouldCalculateCorrectWithSmallAndBigCashflows()
        {
            var service = new InterestRateCalculatorService();

            service.CalculateXNPV(_cfs, R).Should().Be(2086.6476020315416570634272814m);
            service.CalculateXNPV(_negcfs, 0.5).Should().Be(-10.148147600710372651326920258m);
            service.CalculateXNPV(_bigcfs, 0.3).Should().Be(4.9923725815954514810351876895m);
        }

        [TestMethod]
        [Description("FindBrackets should find the brackets")]
        public void FindBracketsShouldFindBracketWithSmallAndBigCashflows()
        {
            var service = new InterestRateCalculatorService();

            var brackets = service.FindBrackets(service.CalculateXNPV, _cfs);
            brackets.First.Should().BeLessThan(0.3733);
            brackets.Second.Should().BeGreaterThan(0.3733);

            brackets = service.FindBrackets(service.CalculateXNPV, _bigcfs);
            brackets.First.Should().BeLessThan(0.5196);
            brackets.Second.Should().BeGreaterThan(0.5196);

            brackets = service.FindBrackets(service.CalculateXNPV, _negcfs);
            brackets.First.Should().BeLessThan(-0.6059);
            brackets.Second.Should().BeGreaterThan(-0.6059);
        }

        [TestMethod]
        [Description("CalculateXIRR should return correct value for mid cashflows")]
        public void CalculateXIRRShouldReturnCorrectValueForMidCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_cfs, Tolerance, MaxIters);

            irr.Value.Should().BeApproximately(0.3733, 0.001);
            irr.Kind.Should().Be(ApproximateResultKind.ApproximateSolution);
        }

        [TestMethod]
        [Description("CalculateXIRR should return correct value for big cashflows")]
        public void CalculateXIRRShouldReturnCorrectValueForBigCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_bigcfs, Tolerance, MaxIters);

            irr.Value.Should().BeApproximately(0.5196, 0.001);
            irr.Kind.Should().Be(ApproximateResultKind.ApproximateSolution);
        }

        [TestMethod]
        [Description("CalculateXIRR should return correct value for negative cashflows")]
        public void CalculateXIRRShouldReturnCorrectValueForNegativeCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_negcfs, Tolerance, MaxIters);

            irr.Value.Should().BeApproximately(-0.6059, 0.001);
            irr.Kind.Should().Be(ApproximateResultKind.ApproximateSolution);
        }

        [TestMethod]
        [Description("CalculateXIRR should return no value for same day and same cashflows")]
        public void CalculateXIRRShouldReturnNoValueForSameDayAndSameCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_samedaysamecfs, Tolerance, MaxIters);

            irr.Kind.Should().Be(ApproximateResultKind.NoSolutionWithinTolerance);
        }

        [TestMethod]
        [Description("CalculateXIRR should return no value for same day but different cashflows")]
        public void CalculateXIRRShouldReturnNoValueForSameDayButDifferentCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_samedaydifferentcfs, Tolerance, MaxIters);

            irr.Kind.Should().Be(ApproximateResultKind.NoSolutionWithinTolerance);
        }

        [TestMethod]
        [Description("CalculateXIRR should return correct value for big rate cashflows")]
        public void CalculateXIRRShouldReturnCorrectValueForBigRateCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_bigratecfs, Tolerance, MaxIters);

            irr.Value.Should().BeApproximately(4.40140, 0.001);
            irr.Kind.Should().Be(ApproximateResultKind.ApproximateSolution);
        }

        [TestMethod]
        [Description("CalculateXIRR should return correct value for zero rate cashflows")]
        public void CalculateXIRRShouldReturnCorrectValueForZeroRateCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_zeroRate, Tolerance, MaxIters);

            irr.Value.Should().BeApproximately(0, 0.001);
            irr.Kind.Should().Be(ApproximateResultKind.ApproximateSolution);
        }

        [TestMethod]
        [Description("CalculateXIRR should return correct value for double negative cashflows")]
        public void CalculateXIRRShouldReturnCorrectValueForDoubleNegativeCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_doubleNegative, Tolerance, MaxIters);

            irr.Value.Should().BeApproximately(-0.537055, 0.001);
            irr.Kind.Should().Be(ApproximateResultKind.ApproximateSolution);
        }

        [TestMethod]
        [Description("CalculateXIRR should return no value for bad double negative cashflows")]
        public void CalculateXIRRShouldReturnNoValueForBadDoubleNegativeCashflows()
        {
            var service = new InterestRateCalculatorService();
            var irr = service.CalculateXIRR(_badDoubleNegative, Tolerance, MaxIters);

            irr.Kind.Should().Be(ApproximateResultKind.NoSolutionWithinTolerance);
        }
    }
}