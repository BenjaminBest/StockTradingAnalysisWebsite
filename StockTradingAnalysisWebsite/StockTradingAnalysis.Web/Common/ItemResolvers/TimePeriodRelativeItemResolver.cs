using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Web.Common.Filter;
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

        /// <summary>
        /// Resolves the actual filter bases on the item value which is used in the sectionlist items
        /// </summary>
        /// <param name="itemValue">The item key.</param>
        /// <returns>Filter for transations</returns>
        public static IEnumerable<ITransactionFilter> ResolveFilter(string itemValue)
        {
            if (string.IsNullOrEmpty(itemValue))
                return Enumerable.Empty<ITransactionFilter>();

            if (itemValue.Equals("FilterAll"))
                return Enumerable.Empty<ITransactionFilter>();

            var dateService = DependencyResolver.Current.GetService<IDateCalculationService>();

            var startDate = DateTime.MinValue;
            var endDate = DateTime.MaxValue;

            if (itemValue.Equals("FilterCurrentMonth"))
            {
                startDate = dateService.GetStartAndEndDateOfMonth(DateTime.Now, out endDate);
            }
            else if (itemValue.Equals("FilterCurrentWeek"))
            {
                startDate = dateService.GetStartAndEndDateOfWeek(DateTime.Now, out endDate);
            }
            else if (itemValue.Equals("Filter2Weeks"))
            {
                startDate = dateService.GetStartAndEndDateOf2Weeks(DateTime.Now, out endDate);
            }
            else if (itemValue.Equals("Filter2Months"))
            {
                startDate = dateService.GetStartAndEndDateOfMonth(DateTime.Now, out endDate).AddMonths(-1);
            }
            else if (itemValue.Equals("Filter3Months"))
            {
                startDate = dateService.GetStartAndEndDateOfMonth(DateTime.Now, out endDate).AddMonths(-2);
            }
            else if (itemValue.Equals("FilterCurrentQuarter"))
            {
                startDate = dateService.GetStartAndEndDateOfQuarter(DateTime.Now, out endDate);
            }
            else if (itemValue.Equals("FilterCurrentYear"))
            {
                startDate = dateService.GetStartAndEndDateOfYear(DateTime.Now, out endDate);
            }
            else if (itemValue.Equals("FilterLastYear"))
            {
                startDate = dateService.GetStartAndEndDateOfYear(DateTime.Now.AddYears(-1), out endDate);
            }

            return new List<ITransactionFilter>
            {
                new TransactionStartDateFilter(startDate),
                new TransactionEndDateFilter(endDate)
            };
        }
    }
}