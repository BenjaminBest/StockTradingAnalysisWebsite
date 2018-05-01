namespace StockTradingAnalysis.Web.Common.Formatting
{
    /// <summary>
    /// Extension methods for the style type.
    /// </summary>
    public static class StyleExtensions
    {
        /// <summary>
        /// Returns the css class for the given style type.
        /// </summary>
        /// <param name="styleType">Type of the style.</param>
        /// <returns></returns>
        public static string CssClass(this StyleType styleType)
        {
            var memInfo = typeof(StyleType).GetMember(styleType.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(StyleTypeCssClassAttribute), false);
            return ((StyleTypeCssClassAttribute)attributes[0]).CssClass;
        }
    }
}