using System.Collections.Generic;
using System.Web.Mvc;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
    /// <summary>
    /// The interface ISelectItem defines an resolver for a specific select list
    /// </summary>
    public interface ISelectItemResolver
    {
        /// <summary>
        /// Gets the identifier of this resolver
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Returns a list with all selectable items
        /// </summary>
        /// <returns>Filter items</returns>
        IEnumerable<SelectListItem> GetItems();
    }
}