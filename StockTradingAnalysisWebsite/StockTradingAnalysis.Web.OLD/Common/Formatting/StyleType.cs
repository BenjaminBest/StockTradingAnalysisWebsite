using System;

namespace StockTradingAnalysis.Web.Common.Formatting
{
    /// <summary>
    /// Enumeration StyleType defines the different styles available
    /// </summary>
    [Flags]
    public enum StyleType
    {
        /// <summary>
        /// The undefined
        /// </summary>
        [StyleTypeCssClass("")]
        Undefined,
        /// <summary>
        /// The primary style
        /// </summary>
        [StyleTypeCssClass("primary")]
        Primary,
        /// <summary>
        /// The secondary style
        /// </summary>
        [StyleTypeCssClass("secondary")]
        Secondary,
        /// <summary>
        /// The success style
        /// </summary>
        [StyleTypeCssClass("success")]
        Success,
        /// <summary>
        /// The danger style
        /// </summary>
        [StyleTypeCssClass("danger")]
        Danger,
        /// <summary>
        /// The warning style
        /// </summary>
        [StyleTypeCssClass("warning")]
        Warning,
        /// <summary>
        /// The information style
        /// </summary>
        [StyleTypeCssClass("info")]
        Info,
        /// <summary>
        /// The dark style
        /// </summary>
        [StyleTypeCssClass("dark")]
        Dark,
        /// <summary>
        /// The light style
        /// </summary>
        [StyleTypeCssClass("light")]
        Light
    }
}