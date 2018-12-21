using System;

namespace StockTradingAnalysis.Web.Common.Formatting
{
    /// <summary>
    /// The attribute StyleTypeCssClassAttribute is used to define a css class for a enumeration part.
    /// </summary>
    public class StyleTypeCssClassAttribute : Attribute
    {
        /// <summary>
        /// Gets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleTypeCssClassAttribute"/> class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        public StyleTypeCssClassAttribute(string cssClass)
        {
            CssClass = cssClass;
        }
    }
}