using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.ItemResolvers
{
	/// <summary>
	/// The class ItemResolverRegistry defines a registry to maintain and hold all item resolvers <seealso cref="ISelectItemResolver"/>
	/// </summary>
	public class ItemResolverRegistry : ISelectItemResolverRegistry
	{
		private readonly IEnumerable<ISelectItemResolver> _resolvers;

		public ItemResolverRegistry(IEnumerable<ISelectItemResolver> resolvers)
		{
			_resolvers = resolvers;
		}

		/// <summary>
		/// Returns all items wich are assigned to the given <paramref name="identifier"/>
		/// </summary>
		/// <param name="identifier">The identifier for a select list item</param>
		/// <returns>List of items</returns>
		public IEnumerable<SelectListItem> GetItems(string identifier)
		{
			var resolver = _resolvers.FirstOrDefault(r => r.Identifier == identifier);

			return resolver != null ? resolver.GetItems() : new List<SelectListItem>();
		}
	}
}