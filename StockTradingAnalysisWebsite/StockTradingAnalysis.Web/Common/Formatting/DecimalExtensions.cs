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
            var cssClass = value > 0 ? StyleType.Success : StyleType.Danger;
            cssClass = value == 0 ? StyleType.Warning : cssClass;

            return cssClass;
        }

        /// <summary>
        /// Returns the style type for the given decimal based on the <paramref name="value"/> which is a percentage value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StyleType StyleTypeForPercentage(this decimal value)
        {
            var cssClass = value > 0 ? StyleType.Success : StyleType.Danger;
            cssClass = value >= 0 && value < 1 ? StyleType.Warning : cssClass;

            return cssClass;
        }

        /// <summary>
        /// Returns the style type for the given decimal based on the <paramref name="value"/> which is an efficiency value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StyleType StyleTypeForEfficiency(this decimal value)
        {
            var cssClass = value > 80 ? StyleType.Success : StyleType.Danger;
            cssClass = value >= 60 && value <= 80 ? StyleType.Warning : cssClass;

            return cssClass;
        }
    }
}