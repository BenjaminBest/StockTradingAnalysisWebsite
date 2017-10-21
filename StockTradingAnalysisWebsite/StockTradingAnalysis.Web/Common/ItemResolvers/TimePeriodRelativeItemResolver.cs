using System.Collections.Generic;
using System.Web.Mvc;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.ItemResolvers
{
    /// <summary>
    /// Class TimePeriodRelativeItemResolver defines an item resolver over time, but relative (last year, last month)
    /// </summary>
    public class TimePeriodRelativeItemResolver : ISelectItemResolver
    {
        /// <summary>
        /// Gets the identifier of this resolver
        /// </summary>
        public string Identifier => "TimePeriodRelative";

        /// <summary>
        /// Returns a list with all items
        /// </summary>
        /// <returns>Items</returns>
        public IEnumerable<SelectListItem> GetItems()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem {Text = Resources.ViewTextFilterAll, Value = "FilterAll", Selected = true},
                new SelectListItem {Text = Resources.ViewTextFilterMonth, Value = "FilterCurrentMonth"},
                new SelectListItem {Text = Resources.ViewTextFilterCurrentWeek, Value = "FilterCurrentWeek"},
                new SelectListItem {Text = Resources.ViewTextFilter2Weeks, Value = "Filter2Weeks"},
                new SelectListItem {Text = Resources.ViewTextFilter2Months, Value = "Filter2Months"},
                new SelectListItem {Text = Resources.ViewTextFilter3Months, Value = "Filter3Months"},
                new SelectListItem {Text = Resources.ViewTextFilterQuarter, Value = "FilterCurrentQuarter"},
                new SelectListItem {Text = Resources.ViewTextFilterYear, Value = "FilterCurrentYear"},
                new SelectListItem {Text = Resources.ViewTextFilterLastYear, Value = "FilterLastYear"}
            };

            return items;
        }
    }
}