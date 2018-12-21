using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockTradingAnalysis.Domain.CQRS.Query.Filter;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Filter;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.ItemResolvers
{
	/// <summary>
	/// Class StockTypeItemResolver defines an item resolver over the different stock types
	/// </summary>
	public class StockTypeItemResolver : ISelectItemResolver
	{
		private readonly IQueryDispatcher _queryDispatcher;

		public StockTypeItemResolver(IQueryDispatcher queryDispatcher)
		{
			_queryDispatcher = queryDispatcher;
		}

		/// <summary>
		/// Gets the identifier of this resolver
		/// </summary>
		public string Identifier => "StockType";

		/// <summary>
		/// Returns a list with all items
		/// </summary>
		/// <returns>Items</returns>
		public IEnumerable<SelectListItem> GetItems()
		{
			var items = new List<SelectListItem>();

			var types = _queryDispatcher.Execute(new StockTypeInUseSearchQuery(string.Empty)).Take(10);

			items.Add(new SelectListItem { Text = Resources.ViewTextFilterAll, Value = "" });
			items.AddRange(types.Select(stock => new SelectListItem { Text = stock, Value = stock }));

			return items;
		}

		/// <summary>
		/// Resolves the actual filter bases on the item value which is used in the sectionlist items
		/// </summary>
		/// <param name="itemValue">The item key.</param>
		/// <returns>Filter for transations</returns>
		public static IEnumerable<ITransactionFilter> ResolveFilter(string itemValue)
		{
			return string.IsNullOrEmpty(itemValue) ? Enumerable.Empty<ITransactionFilter>() : new List<ITransactionFilter> { new TransactionStockTypeFilter(itemValue) };
		}
	}
}