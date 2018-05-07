namespace StockTradingAnalysis.Web.Common.Formatting
{
    /// <summary>
    /// Extension methods for decimals.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Returns the style type for the given decimal based on the <paramref name="value"/> which is a currency.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StyleType StyleTypeForCurrency(this decimal value)
        {
            return new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenEqualThan(0, StyleType.Warning)
                .WhenGreaterThan(0, StyleType.Success)
                .Compile();
        }

        /// <summary>
        /// Returns the style type for the given decimal based on the <paramref name="value"/> which is a percentage value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StyleType StyleTypeForPercentage(this decimal value)
        {
            return new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenSmallerThan(0, StyleType.Danger)
                .WhenGreaterOrEqualThan(0, StyleType.Warning)
                .WhenGreaterOrEqualThan(1, StyleType.Success)
                .Compile();
        }

        /// <summary>
        /// Returns the style type for the given decimal based on the <paramref name="value"/> which is an efficiency value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StyleType StyleTypeForEfficiency(this decimal value)
        {
            return new StyleTypeBuilder<decimal>(value)
                .Default(StyleType.Info)
                .WhenGreaterOrEqualThan(0, StyleType.Danger)
                .WhenGreaterOrEqualThan(60, StyleType.Warning)
                .WhenGreaterOrEqualThan(80, StyleType.Success)
                .Compile();
        }
    }
}