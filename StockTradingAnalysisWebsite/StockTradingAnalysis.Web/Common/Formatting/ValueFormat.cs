using StockTradingAnalysis.Web.Common.Enumerations;

namespace StockTradingAnalysis.Web.Common.Formatting
{
    /// <summary>
    /// Class ValueFormat ist used to determine the css class for a given value
    /// </summary>
    public static class ValueFormat
    {
        /// <summary>
        /// Returns the css class for the given value and format
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="format">Format</param>
        ///  <param name="method">Style evaluation method</param>
        public static string GetCssClass(decimal value, StatisticsKPIsFormat format,
            StatisticsKPIsStyleCalcFormat method)
        {
            if (method == StatisticsKPIsStyleCalcFormat.No)
                return "standardPerf";

            if (method == StatisticsKPIsStyleCalcFormat.Positive)
                return "greenPerf";

            if (method == StatisticsKPIsStyleCalcFormat.Negative)
                return "redPerf";

            //Dynamically

            if (format == StatisticsKPIsFormat.Currency)
            {
                var cssClass = value > 0 ? "greenPerf" : "redPerf";
                cssClass = value == 0 ? "orangePerf" : cssClass;

                return cssClass;
            }

            if (format == StatisticsKPIsFormat.Percentage)
            {
                var cssClass = value > 0 ? "greenPerf" : "redPerf";
                cssClass = value >= 0 && value < 1 ? "orangePerf" : cssClass;

                return cssClass;
            }

            if (format == StatisticsKPIsFormat.Efficiency)
            {
                var cssClass = value > 80 ? "greenPerf" : "redPerf";
                cssClass = value >= 60 && value <= 80 ? "orangePerf" : cssClass;

                return cssClass;
            }

            return string.Empty;
        }
    }
}