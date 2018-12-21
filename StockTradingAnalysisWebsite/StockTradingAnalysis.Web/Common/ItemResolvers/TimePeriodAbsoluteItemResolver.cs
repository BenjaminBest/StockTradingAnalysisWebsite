using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.ItemResolvers
{
	/// <summary>
	/// Class TimePeriodAbsoluteItemResolver defines an item resolver over time (years, months..)
	/// </summary>
	public class TimePeriodAbsoluteItemResolver : ISelectItemResolver
	{
		/// <summary>
		/// Gets the identifier of this resolver
		/// </summary>
		public string Identifier => "TimePeriodAbsolute";

		/// <summary>
		/// Returns a list with all items
		/// </summary>
		/// <returns>Items</returns>
		public IEnumerable<SelectListItem> GetItems()
		{
			var items = new List<SelectListItem>
			{
				new SelectListItem {Text = Resources.ViewTextFilterAll, Value = "All", Selected = true},
				new SelectListItem {Text = Resources.ViewTextYears, Value = "FilterYears"},
				new SelectListItem {Text = Resources.ViewTextFilterQuarters, Value = "FilterQuarters"},
				new SelectListItem {Text = Resources.ViewTextFilterMonths, Value = "FilterMonths"},
				new SelectListItem {Text = Resources.ViewTextFilterWeeks, Value = "FilterWeeks"},
				new SelectListItem {Text = Resources.ViewTextFilterDays, Value = "FilterDays"}
			};

			return items;
		}
	}
}