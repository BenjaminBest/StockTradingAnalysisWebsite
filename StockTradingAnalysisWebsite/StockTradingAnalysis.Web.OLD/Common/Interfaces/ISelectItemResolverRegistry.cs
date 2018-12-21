using System.Collections.Generic;
using System.Web.Mvc;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
    /// <summary>
    /// The interface ISelectItemResolverRegistry defines a registry to maintain and hold all item resolvers <seealso cref="ISelectItemResolver"/>
    /// </summary>
    public interface ISelectItemResolverRegistry
    {
        /// <summary>
        /// Returns all items wich are assigned to the given <paramref name="identifier"/>
        /// </summary>
        /// <param name="identifier">The identifier for a select list item</param>
        /// <returns>List of items</returns>
        IEnumerable<SelectListItem> GetItems(string identifier);
    }
}