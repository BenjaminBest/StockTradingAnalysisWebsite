using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Web.Common.Formatting;

namespace StockTradingAnalysis.Web.Tests.Common.Formatting
{
    [TestClass]
    public class StyleTypeBuilderTests
    {
        [TestMethod]
        public void StyleTypeBuilderCompile_ShouldReturnStyle_WhenEqualThan()
        {
            var value = 0;

            var result = new StyleTypeBuilder<int>(value)
                .Default(StyleType.Info)
                .WhenEqualThan(0, StyleType.Light)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Light);
        }

        [TestMethod]
        public void StyleTypeBuilderCompile_ShouldReturnStyle_WhenSmallerThan()
        {
            var value = -1;

            var result = new StyleTypeBuilder<int>(value)
                .Default(StyleType.Info)
                .WhenEqualThan(0, StyleType.Light)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Danger);
        }

        [TestMethod]
        public void StyleTypeBuilderCompile_ShouldReturnStyle_WhenGreaterThan()
        {
            var value = 1;

            var result = new StyleTypeBuilder<int>(value)
                .Default(StyleType.Info)
                .WhenEqualThan(0, StyleType.Light)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Success);
        }

        [TestMethod]
        public void StyleTypeBuilderCompile_ShouldReturnDefaultStyle_WhenNoMatch()
        {
            var value = 0;

            var result = new StyleTypeBuilder<int>(value)
                .Default(StyleType.Info)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Info);
        }

        [TestMethod]
        public void StyleTypeBuilderCompile_ShouldReturnStyle_WhenSmallerOrEqualThan()
        {
            var value = 0;

            var result = new StyleTypeBuilder<int>(value)
                .Default(StyleType.Info)
                .WhenSmallerOrEqualThan(0, StyleType.Danger)
                .WhenGreaterOrEqualThan(1, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Danger);
        }

        [TestMethod]
        public void StyleTypeBuilderCompile_ShouldReturnStyle_WhenGreaterOrEqualThan()
        {
            var value = 1;

            var result = new StyleTypeBuilder<int>(value)
                .Default(StyleType.Info)
                .WhenSmallerOrEqualThan(0, StyleType.Danger)
                .WhenGreaterOrEqualThan(1, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Success);
        }

        [TestMethod]
        public void StyleTypeBuilderCompile_ShouldReturnStyle_WhenThresholdWasNotReached()
        {
            var value = 1;

            var result = new StyleTypeBuilder<int>(value)
                .Default(StyleType.Info)
                .WhenEqualThan(0, StyleType.Light)
                .WhenGreaterThan(0, StyleType.Success)
                .WhenGreaterThan(1, StyleType.Danger)
                .Compile();

            result.Should().Be(StyleType.Success);
        }

        [TestMethod]
        public void StyleTypeBuilderCompile_ShouldReturnStyle_WhenThresholdWasReached()
        {
            var value = 2;

            var result = new StyleTypeBuilder<int>(value)
                .Default(StyleType.Info)
                .WhenEqualThan(0, StyleType.Light)
                .WhenGreaterThan(0, StyleType.Success)
                .WhenGreaterThan(1, StyleType.Danger)
                .Compile();

            result.Should().Be(StyleType.Danger);
        }

        [TestMethod]
        public void StyleTypeBuilderCompileDecimal_ShouldReturnStyle_WhenEqualThan()
        {
            const decimal value = 0.0m;

            var result = new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenEqualThan(0, StyleType.Light)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Light);
        }

        [TestMethod]
        public void StyleTypeBuilderCompileDecimal_ShouldReturnStyle_WhenSmallerThan()
        {
            const decimal value = -1.0m;

            var result = new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenEqualThan(0, StyleType.Light)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Danger);
        }

        [TestMethod]
        public void StyleTypeBuilderCompileDecimal_ShouldReturnStyle_WhenGreaterThan()
        {
            const decimal value = 1.0m;

            var result = new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenEqualThan(0, StyleType.Light)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Success);
        }

        [TestMethod]
        public void StyleTypeBuilderCompileDecimal_ShouldReturnDefaultStyle_WhenNoMatch()
        {
            const decimal value = 0.0m;

            var result = new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Info);
        }

        [TestMethod]
        public void StyleTypeBuilderCompileDecimal_ShouldReturnStyle_WhenSmallerOrEqualThan()
        {
            const decimal value = 0.0m;

            var result = new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenSmallerOrEqualThan(0, StyleType.Danger)
                .WhenGreaterOrEqualThan(1, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Danger);
        }

        [TestMethod]
        public void StyleTypeBuilderCompileDecimal_ShouldReturnStyle_WhenGreaterOrEqualThan()
        {
            const decimal value = 1.0m;

            var result = new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenSmallerOrEqualThan(0, StyleType.Danger)
                .WhenGreaterOrEqualThan(1, StyleType.Success)
                .Compile();

            result.Should().Be(StyleType.Success);
        }
    }
}
